using AdventOfCode2022.CSharp.Sixteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySixteenTests
{
    [Fact]
    public void CalculateMaxPressureReleased()
    {
        string filePath = @"Sixteen\DaySixteenTestInputA.txt";
        var sut = new DaySixteen();
        var result = sut.CalculateMaxPressureReleased(filePath, false);

        Assert.Equal(1651, result);
    }

    [Fact]
    public void CalculateMaxPressureReleased_WithHellp()
    {
        string filePath = @"Sixteen\DaySixteenTestInputA.txt";
        var sut = new DaySixteen();
        var result = sut.CalculateMaxPressureReleased(filePath, true);

        Assert.Equal(1707, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartA();

        Assert.Equal("1584", result);
    }

    // Takes 1.3 min, so commenting out
    // Struggled on this Day too much to refactor
    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySixteen();
        var result = sut.PartB();

        Assert.Equal("2052", result);
    }
    */
}