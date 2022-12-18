using AdventOfCode2022.CSharp.Seventeen;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySeventeenTests
{
    private readonly string testInput = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";

    [Fact]
    public void RunSimulationAndCalculateHighestY()
    {
        var sut = new DaySeventeen();
        var result = sut.RunSimulationAndCalculateHighestY(testInput, 2022);

        Assert.Equal(3068, result);
    }

    [Fact]
    public void RunBigSimulationAndCalculateHighestY()
    {
        var sut = new DaySeventeen();
        var result = sut.RunBigSimulationAndCalculateHighestY(testInput, 1000000000000);

        Assert.Equal(1514285714288, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartA();

        Assert.Equal("3168", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeventeen();
        var result = sut.PartB();

        Assert.Equal("1554117647070", result);
    }
}