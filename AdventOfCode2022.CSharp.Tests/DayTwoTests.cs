using AdventOfCode2022.CSharp.Two;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwoTests
{
    [Fact]
    public void ScoreGame()
    {
        string filePath = @"Two\DayTwoTestInputA.txt";

        var sut = new DayTwo();
        var result = sut.ScoreGame(filePath, false);

        Assert.Equal(15, result);
    }

    [Fact]
    public void ScoreGame_DetermineShape()
    {
        string filePath = @"Two\DayTwoTestInputA.txt";

        var sut = new DayTwo();
        var result = sut.ScoreGame(filePath, true);

        Assert.Equal(12, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartA();

        Assert.Equal("14375", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwo();
        var result = sut.PartB();

        Assert.Equal("10274", result);
    }
}