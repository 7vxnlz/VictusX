using GHelper.Hardware.Hp;
using Xunit;

namespace VictusX.Tests.Hardware.Hp;

public sealed class HpReadOnlyTelemetryTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 4, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void MissingSources_ShowUnknownWithoutInventingSensorValues()
    {
        var snapshot = new HpReadOnlyTelemetryProvider(new FakeSource()).Capture(Now);
        var display = HpReadOnlyTelemetryFormatter.Format(snapshot, Now, null, true);

        Assert.Null(snapshot.CpuLoadPercent);
        Assert.Null(snapshot.BatteryPercent);
        Assert.Null(snapshot.BatteryPresent);
        Assert.Null(snapshot.AcOnline);
        Assert.Null(snapshot.Charging);
        Assert.Null(snapshot.CpuTemperatureCelsius);
        Assert.Null(snapshot.GpuTemperatureCelsius);
        Assert.Null(snapshot.FanRpm);
        Assert.Contains("Unavailable", display.Cpu);
        Assert.Contains("Unknown", display.Cpu);
        Assert.Contains("Unavailable", display.Gpu);
        Assert.Contains("Fan RPM: Unavailable", display.FanAndDevice);
        Assert.Contains("Device: Unknown", display.FanAndDevice);
        Assert.Equal("Unavailable | AC unknown", display.Battery);
    }

    [Fact]
    public void CpuLoad_UsesDeltasAndResetsAfterFailurePauseOrExplicitReset()
    {
        var source = new FakeSource { Cpu = new(100, 200, 100) };
        var provider = new HpReadOnlyTelemetryProvider(source);
        Assert.Null(provider.Capture(Now).CpuLoadPercent);
        source.Cpu = new(150, 300, 200);
        Assert.Equal(75, provider.Capture(Now.AddSeconds(1)).CpuLoadPercent);
        source.Cpu = new(250, 400, 200);
        Assert.Equal(0, provider.Capture(Now.AddSeconds(2)).CpuLoadPercent);
        source.ThrowCpu = true;
        Assert.Null(provider.Capture(Now.AddSeconds(3)).CpuLoadPercent);
        source.ThrowCpu = false;
        source.Cpu = new(250, 500, 200);
        Assert.Null(provider.Capture(Now.AddSeconds(4)).CpuLoadPercent);
        source.Cpu = new(250, 600, 200);
        Assert.Equal(100, provider.Capture(Now.AddSeconds(5)).CpuLoadPercent);
        source.Cpu = new(250, 700, 200);
        Assert.Null(provider.Capture(Now.AddSeconds(20)).CpuLoadPercent);
        provider.Reset();
        source.Cpu = new(250, 800, 200);
        Assert.Null(provider.Capture(Now.AddSeconds(21)).CpuLoadPercent);
    }

    [Fact]
    public void InvalidOrNonAdvancingCpuSamples_AreUnknown()
    {
        var source = new FakeSource { Cpu = new(100, 200, 100) };
        var provider = new HpReadOnlyTelemetryProvider(source);
        provider.Capture(Now);
        Assert.Null(provider.Capture(Now.AddSeconds(1)).CpuLoadPercent);
        source.Cpu = new(300, 201, 100);
        Assert.Null(provider.Capture(Now.AddSeconds(2)).CpuLoadPercent);
        source.Cpu = new(1, 2, 1);
        Assert.Null(provider.Capture(Now.AddSeconds(3)).CpuLoadPercent);
        source.Cpu = new(1, 3, 2);
        Assert.Null(provider.Capture(Now.AddSeconds(-1)).CpuLoadPercent);
    }

    [Theory]
    [InlineData(1, 8, 75, true, 75, true, true)]
    [InlineData(0, 1, 50, true, 50, false, false)]
    [InlineData(1, 1, 100, true, 100, true, false)]
    [InlineData(0, 4, 0, true, 0, false, false)]
    [InlineData(1, 128, 100, false, null, true, null)]
    [InlineData(255, 255, 255, null, null, null, null)]
    [InlineData(1, 255, 80, null, null, true, null)]
    [InlineData(1, 8, 255, true, null, true, true)]
    [InlineData(1, 8, 101, true, null, true, true)]
    [InlineData(0, 8, 50, true, 50, false, null)]
    public void PowerStatus_HandlesUnknownAbsentAndContradictoryValues(
        byte ac, byte flags, byte percentage, bool? expectedPresent, int? expectedPercent,
        bool? expectedAc, bool? expectedCharging)
    {
        var source = new FakeSource { Power = new(ac, flags, percentage) };
        var snapshot = new HpReadOnlyTelemetryProvider(source).Capture(Now);
        Assert.Equal(expectedPresent, snapshot.BatteryPresent);
        Assert.Equal(expectedPercent, snapshot.BatteryPercent);
        Assert.Equal(expectedAc, snapshot.AcOnline);
        Assert.Equal(expectedCharging, snapshot.Charging);
    }

    [Fact]
    public void PowerFailure_DoesNotPreventCpuSamplingOrRetainOldBatteryValues()
    {
        var source = new FakeSource { Cpu = new(10, 20, 10), Power = new(1, 8, 75) };
        var provider = new HpReadOnlyTelemetryProvider(source);
        Assert.Equal(75, provider.Capture(Now).BatteryPercent);
        source.ThrowPower = true;
        source.Cpu = new(15, 30, 20);
        var snapshot = provider.Capture(Now.AddSeconds(1));
        Assert.Equal(75, snapshot.CpuLoadPercent);
        Assert.Null(snapshot.BatteryPercent);
        Assert.Null(snapshot.AcOnline);
    }

    [Fact]
    public void StaleReadings_AreUnavailableWithLastPollTimePreserved()
    {
        var snapshot = new HpReadOnlyTelemetrySnapshot(Now, 30, 75, true, true, true);
        var fresh = HpReadOnlyTelemetryFormatter.Format(snapshot, Now, true, false);
        Assert.Contains("30% load", fresh.Cpu);
        Assert.Equal("75% | AC | Charging", fresh.Battery);
        Assert.Contains("GetSystemTimes", fresh.Summary);
        Assert.Contains("GetSystemPowerStatus", fresh.Summary);

        var stale = HpReadOnlyTelemetryFormatter.Format(snapshot, Now.AddMinutes(1), true, false);
        Assert.Contains("Stale", stale.Summary);
        Assert.Contains("2026-09-04 12:00:00Z", stale.Summary);
        Assert.DoesNotContain("30%", stale.Cpu);
        Assert.DoesNotContain("75%", stale.Battery);
        Assert.Contains("Unknown", stale.Cpu);
    }

    [Fact]
    public void CachedDeviceEvidence_CannotSupplyRpmTemperatureOrControlValidation()
    {
        var report = new HpDiagnosticReportLoadResult(HpDiagnosticReportLoadStatus.Loaded, new()
        {
            ["LooksLikeHp"] = "true", ["LooksLikeVictus"] = "true",
            ["FanGetLevelDecoded.Fan1RawValue"] = "34", ["FanGetLevelDecoded.Fan2RawValue"] = "0",
            ["SetFanMaxDeviceValidatedInputLength"] = "4"
        });
        var snapshot = new HpReadOnlyTelemetryProvider(new FakeSource()).Capture(Now);
        var display = HpReadOnlyTelemetryFormatter.Format(snapshot, Now, report.GetHpVictusDetected(), true);

        Assert.Equal("Fan RPM: Unavailable | HP Victus detected (cached)", display.FanAndDevice);
        Assert.Null(snapshot.FanRpm);
        Assert.Null(snapshot.CpuTemperatureCelsius);
        Assert.Null(snapshot.GpuTemperatureCelsius);
        Assert.Contains("Normal fan control: NO-GO", display.Summary);
        Assert.Contains("raw-only", display.Summary);
        Assert.Null(HpFanMaxDryRunReport.CreateDefaultBlocked().SetFanMaxDeviceValidatedInputLength);
        Assert.DoesNotContain(typeof(HpReadOnlyTelemetrySnapshot).GetProperties(),
            property => property.Name.Contains("Validated", StringComparison.Ordinal));
    }

    [Fact]
    public void GpuReadingHasIndependentFreshnessAndDoesNotPopulateCpuOrFans()
    {
        var snapshot = new HpReadOnlyTelemetrySnapshot(Now, 10, 75, true, true, false)
        { GpuTemperature = new(Now, 62) };
        Assert.Equal("Temp: 62 C", HpReadOnlyTelemetryFormatter.Format(snapshot, Now, true, false).Gpu);
        Assert.Null(snapshot.CpuTemperatureCelsius);
        Assert.Null(snapshot.FanRpm);
        var staleGpu = snapshot with { PolledAt = Now.AddSeconds(6) };
        Assert.Contains("Unavailable", HpReadOnlyTelemetryFormatter.Format(staleGpu, Now.AddSeconds(6), true, false).Gpu);
        Assert.Contains("Unavailable", HpReadOnlyTelemetryFormatter.Format(snapshot, Now.AddSeconds(-1), true, false).Gpu);
    }

    [Theory]
    [InlineData(0)] [InlineData(-1)] [InlineData(126)] [InlineData(double.NaN)] [InlineData(double.PositiveInfinity)]
    public void InvalidGpuReadingsAreNotDisplayed(double value)
    {
        var snapshot = new HpReadOnlyTelemetrySnapshot(Now, null, null, null, null, null)
        { GpuTemperature = new(Now, value) };
        Assert.Equal("Temp: Unavailable", HpReadOnlyTelemetryFormatter.Format(snapshot, Now, null, false).Gpu);
    }

    [Fact]
    public void GpuPollingIsBoundedAndResetDiscardsPendingReading()
    {
        using var entered = new ManualResetEventSlim();
        using var release = new ManualResetEventSlim();
        int calls = 0;
        var poller = new HpGpuTemperaturePoller(() =>
        {
            Interlocked.Increment(ref calls);
            entered.Set();
            release.Wait(TimeSpan.FromSeconds(3));
            return 60;
        });
        try
        {
            Assert.Null(poller.Poll(Now));
            Assert.True(entered.Wait(TimeSpan.FromSeconds(2)));
            Assert.Null(poller.Poll(Now.AddSeconds(20)));
            Assert.Equal(1, Volatile.Read(ref calls));
            poller.Reset();
        }
        finally { release.Set(); }
        // A reset cannot expose the old sample; the first newly admitted read has its own timestamp.
        Assert.True(SpinWait.SpinUntil(() => poller.Poll(Now.AddSeconds(21)) is not null, TimeSpan.FromSeconds(3)));
        var fresh = poller.Poll(Now.AddSeconds(21));
        Assert.Equal(Now.AddSeconds(21), fresh!.Value.SampledAt);
        Assert.Equal(2, calls);
        Assert.Null(poller.Poll(Now.AddSeconds(27)));
    }

    [Fact]
    public void GpuFailureClearsPriorSampleWithoutAffectingOsTelemetry()
    {
        int calls = 0;
        var poller = new HpGpuTemperaturePoller(() =>
        {
            if (Interlocked.Increment(ref calls) > 1) throw new InvalidOperationException("driver unavailable");
            return 65;
        });
        var provider = new HpReadOnlyTelemetryProvider(new FakeSource { Power = new(1, 8, 50) }, poller);
        Assert.True(SpinWait.SpinUntil(() => provider.Capture(Now).GpuTemperature is not null, TimeSpan.FromSeconds(2)));
        Assert.Equal(1, calls);
        Assert.True(SpinWait.SpinUntil(() => provider.Capture(Now.AddSeconds(2)).GpuTemperature is null, TimeSpan.FromSeconds(2)));
        Assert.Equal(50, provider.Capture(Now.AddSeconds(2)).BatteryPercent);
    }

    private sealed class FakeSource : IHpReadOnlyTelemetrySource
    {
        public HpCpuTimes? Cpu { get; set; }
        public HpPowerStatus? Power { get; set; }
        public bool ThrowCpu { get; set; }
        public bool ThrowPower { get; set; }
        public HpCpuTimes? ReadCpuTimes() => ThrowCpu ? throw new InvalidOperationException("Unavailable") : Cpu;
        public HpPowerStatus? ReadPowerStatus() => ThrowPower ? throw new InvalidOperationException("Unavailable") : Power;
    }
}
