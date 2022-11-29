using AdventOfCode2022.CSharp.OldOne;
using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Tests;

public class OldDayOneTests
{
    [Fact]
    public void CountSweepIncreases()
    {
        string filePath = @"OldOne\OldDayOneTestInputA.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));

        var sut = new OldDayOne();
        var result = sut.CountSweepIncreases(sweeps);

        Assert.Equal(7, result);
    }

    [Fact]
    public void CountSweepGroupIncreases()
    {
        string filePath = @"OldOne\OldDayOneTestInputA.txt";
        List<int> sweeps = FileUtility.ParseFileToList(filePath, line => int.Parse(line.Trim()));

        var sut = new OldDayOne();
        var result = sut.CountSweepGroupIncreases(sweeps);

        Assert.Equal(5, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new OldDayOne();
        var result = sut.PartA();

        Assert.Equal("1215", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new OldDayOne();
        var result = sut.PartB();

        Assert.Equal("1150", result);
    }
}