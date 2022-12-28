using AdventOfCode2022.CSharp.TwentyFour;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyFourTests
{
    [Fact]
    public void MinutesUntilExit()
    {
        string filePath = @"TwentyFour\DayTwentyFourTestInputA.txt";
        var sut = new DayTwentyFour();
        var result = sut.MinutesUntilExit(filePath);

        Assert.Equal(18, result);
    }

    [Fact]
    public void NewMinutesUntilExitThenBack()
    {
        string filePath = @"TwentyFour\DayTwentyFourTestInputA.txt";
        var sut = new DayTwentyFour();
        var result = sut.MinutesUntilExitThenBack(filePath);

        Assert.Equal(54, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartA();

        Assert.Equal("373", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFour();
        var result = sut.PartB();

        Assert.Equal("997", result);
    }
}