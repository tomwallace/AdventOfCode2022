using AdventOfCode2022.CSharp.Eighteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayEighteenTests
{
    [Fact]
    public void CountOpenSides()
    {
        string filePath = @"Eighteen\DayEighteenTestInputA.txt";
        var sut = new DayEighteen();
        var result = sut.CountOpenSides(filePath);

        Assert.Equal(64, result);
    }

    [Fact]
    public void CountOutsideSides()
    {
        string filePath = @"Eighteen\DayEighteenTestInputA.txt";
        var sut = new DayEighteen();
        var result = sut.CountOutsideSidesWithFlood(filePath);

        Assert.Equal(58, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartA();

        Assert.Equal("4418", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEighteen();
        var result = sut.PartB();

        Assert.Equal("2486", result);
    }
}