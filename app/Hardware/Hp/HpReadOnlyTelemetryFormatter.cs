namespace GHelper.Hardware.Hp;

internal sealed record HpReadOnlyTelemetryDisplay(
    string Cpu, string Gpu, string FanAndDevice, string Battery, string BatteryCare, string Display, string Summary);

internal static class HpReadOnlyTelemetryFormatter
{
    public static HpReadOnlyTelemetryDisplay Format(
        HpReadOnlyTelemetrySnapshot snapshot, DateTimeOffset now, bool? hpVictusDetected, bool cachedIdentity)
    {
        bool fresh = snapshot.PolledAt is { } time && now >= time &&
            now - time <= HpReadOnlyTelemetryProvider.MaximumSampleAge;
        HpReadOnlyTelemetrySnapshot current = fresh ? snapshot : HpReadOnlyTelemetrySnapshot.Unavailable;
        string load = current.CpuLoadPercent is { } percent ? $"{percent}% load" : "Load: Unknown";
        string battery = current.BatteryPresent == false ? "No battery" :
            current.BatteryPercent is { } charge ? $"{charge}%" : "Unavailable";
        string ac = current.AcOnline switch { true => "AC", false => "On battery", _ => "AC unknown" };
        string charging = current.Charging switch { true => "Charging", false => "Not charging", _ => "Charging unknown" };
        string device = hpVictusDetected switch
        {
            true => "HP Victus detected",
            false => "HP Victus not detected",
            _ => "Device: Unknown"
        };
        string identitySource = cachedIdentity ? "cached report" : "startup snapshot";
        string state = snapshot.PolledAt is null ? "Not sampled" : fresh ? "Current" : "Stale";
        string poll = snapshot.PolledAt?.ToUniversalTime().ToString("u") ?? "Unavailable";
        string refreshRate = current.DisplayRefreshRateHz is { } hz ? $"{hz}Hz" : "Unavailable";
        string batteryCare = FormatBatteryCare(current.BatteryCare?.Result);
        bool gpuFresh = current.GpuTemperature is { } gpu && now >= gpu.SampledAt &&
            now - gpu.SampledAt <= HpReadOnlyTelemetryProvider.MaximumSampleAge && gpu.Celsius is > 0 and <= 125;
        string gpuTemperature = gpuFresh
            ? current.GpuTemperature!.Value.Celsius.ToString("0", System.Globalization.CultureInfo.InvariantCulture) + " C"
            : "Unavailable";
        string summary = $"Read-only OS telemetry: {state}; last poll: {poll}\n" +
            $"CPU load: {load} (GetSystemTimes); battery: {battery}, {ac}, {charging} (GetSystemPowerStatus).\n" +
            $"Battery charge limit: {batteryCare} (read-only HP BIOS setting inventory; numeric limits unavailable).\n" +
            $"Display refresh rate: {refreshRate} (Windows current settings for the internal panel when identifiable).\n" +
            $"GPU temperature: {gpuTemperature} (NVIDIA NVAPI GPU-target sensor; optional installed display driver).\n" +
            "CPU temperature: Unavailable; no verified driver-free package sensor.\n" +
            "Fan 1 / Fan 2 RPM: Unavailable; no verified V1 tachometer source; 0x38 is not enabled.\n" +
            $"{device} ({identitySource}); cached fan levels remain raw-only. Normal fan control: NO-GO.";

        string batteryStatus = current.BatteryPresent == false ? $"No battery | {ac}" :
            current.AcOnline == true && current.Charging.HasValue ? $"{battery} | AC | {charging}" : $"{battery} | {ac}";
        return new(
            $"Temp: Unavailable | {load}", $"Temp: {gpuTemperature}",
            $"Fan RPM: Unavailable | {device}" + (cachedIdentity ? " (cached)" : ""),
            batteryStatus, $"Battery care: {batteryCare}", $"Screen: {refreshRate}", summary);
    }

    internal static string FormatBatteryCare(HpBatteryCareProbeResult? result) => result switch
    {
        { Availability: HpBatteryCareAvailability.Supported, Enabled: true } => "Enabled; limit values unavailable",
        { Availability: HpBatteryCareAvailability.Supported, Enabled: false } => "Disabled; limit values unavailable",
        { Availability: HpBatteryCareAvailability.Supported } => "Supported, state unavailable",
        { Availability: HpBatteryCareAvailability.NotExposed } => "Not exposed by HP BIOS settings",
        _ => "Unavailable"
    };
}
