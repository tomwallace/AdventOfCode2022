using AdventOfCode2022.CSharp.Three;
using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Tests;

public class DayThreeTests
{
    [Fact]
    public void GetPriorityScoreSum()
    {
        string filePath = @"Three\DayThreeTestInputA.txt";
        var rucksacks = FileUtility.ParseFileToList(filePath, line => line);
        var sut = new DayThree();
        var result = sut.GetPriorityScoreSum(rucksacks, false);

        Assert.Equal(157, result);
    }

    [Fact]
    public void GetPriorityScoreSum_Groups()
    {
        string filePath = @"Three\DayThreeTestInputA.txt";
        var rucksacks = FileUtility.ParseFileToList(filePath, line => line);
        var sut = new DayThree();
        var result = sut.GetPriorityScoreSum(rucksacks, true);

        Assert.Equal(70, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartA();

        Assert.Equal("7763", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThree();
        var result = sut.PartB();

        Assert.Equal("2569", result);
    }
}