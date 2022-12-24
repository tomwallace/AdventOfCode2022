using AdventOfCode2022.CSharp.TwentyThree;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyThreeTests
{
    [Fact]
    public void CountEmptySpace()
    {
        string filePath = @"TwentyThree\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.CountEmptySpace(filePath, 10);

        Assert.Equal(110, result);
    }

    [Fact]
    public void RoundsUntilNoElfMoved()
    {
        string filePath = @"TwentyThree\DayTwentyThreeTestInputA.txt";
        var sut = new DayTwentyThree();
        var result = sut.RoundsUntilNoElfMoved(filePath);

        Assert.Equal(20, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartA();

        Assert.Equal("4091", result);
    }

    // Uncomment out to run - takes 4.1 min
    /*
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyThree();
        var result = sut.PartB();

        Assert.Equal("1036", result);
    }
    */
}