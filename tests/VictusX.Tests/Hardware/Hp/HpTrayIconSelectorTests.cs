using GHelper.UI;
using Xunit;

namespace VictusX.Tests.Hardware.Hp;

public sealed class HpTrayIconSelectorTests
{
    [Theory]
    [InlineData(2, "Silent", "GHelper.Assets.VictusX.Silent.ico")]
    [InlineData(0, "Balanced", "GHelper.Assets.VictusX.Balanced.ico")]
    [InlineData(1, "Turbo", "GHelper.Assets.VictusX.Turbo.ico")]
    [InlineData(-1, "Default", "GHelper.Assets.VictusX.ico")]
    [InlineData(3, "Default", "GHelper.Assets.VictusX.ico")]
    public void Select_MapsInheritedBaseModeToExpectedHpIcon(
        int basePerformanceMode,
        string expectedKind,
        string expectedResource)
    {
        HpTrayIconKind kind = HpTrayIconSelector.Select(basePerformanceMode);

        Assert.Equal(expectedKind, kind.ToString());
        Assert.Equal(expectedResource, HpTrayIconSelector.GetResourceName(kind));
    }
}
