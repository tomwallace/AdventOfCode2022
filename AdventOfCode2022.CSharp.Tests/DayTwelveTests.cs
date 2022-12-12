using AdventOfCode2022.CSharp.Twelve;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwelveTests
{
    [Fact]
    public void FindMinSteps()
    {
        string filePath = @"Twelve\DayTwelveTestInputA.txt";
        var sut = new DayTwelve();
        var result = sut.FindMinStepsQueue(filePath, false);

        Assert.Equal(31, result);
    }

    [Fact]
    public void FindMinSteps_MultipleStarts()
    {
        string filePath = @"Twelve\DayTwelveTestInputA.txt";
        var sut = new DayTwelve();
        var result = sut.FindMinStepsQueue(filePath, true);

        Assert.Equal(29, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartA();

        Assert.Equal("497", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwelve();
        var result = sut.PartB();

        Assert.Equal("492", result);
    }
}