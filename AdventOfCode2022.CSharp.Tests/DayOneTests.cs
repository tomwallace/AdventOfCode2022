using AdventOfCode2022.CSharp.One;
using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Tests;

public class DayOneTests
{
    [Fact]
    public void CalculateCalorieTotals()
    {
        string filePath = @"One\DayOneTestInputA.txt";
        List<string> data = FileUtility.ParseFileToList(filePath, line => line);

        var sut = new DayOne();
        var result = sut.CalculateCalorieTotals(data);

        Assert.Equal(5, result.Count);
        Assert.Equal(24000, result.Max());
        Assert.Equal(4000, result.Min());
    }

    [Fact]
    public void TotalCaloriesTopThreeElves()
    {
        string filePath = @"One\DayOneTestInputA.txt";
        List<string> data = FileUtility.ParseFileToList(filePath, line => line);

        var sut = new DayOne();
        var result = sut.TotalCaloriesTopThreeElves(data);

        Assert.Equal(45000, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartA();

        Assert.Equal("71924", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayOne();
        var result = sut.PartB();

        Assert.Equal("210406", result);
    }
}