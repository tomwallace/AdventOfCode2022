using AdventOfCode2022.CSharp.Eight;

namespace AdventOfCode2022.CSharp.Tests;

public class DayEightTests
{
    [Fact]
    public void CountOverlapSections()
    {
        string filePath = @"Eight\DayEightTestInputA.txt";
        var sut = new DayEight();
        var result = sut.CountVisibleTrees(filePath);

        Assert.Equal(21, result);
    }

    [Fact]
    public void FindBestScenicScore()
    {
        string filePath = @"Eight\DayEightTestInputA.txt";
        var sut = new DayEight();
        var result = sut.FindBestScenicScore(filePath);

        Assert.Equal(8, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartA();

        Assert.Equal("1816", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEight();
        var result = sut.PartB();

        Assert.Equal("383520", result);
    }
}