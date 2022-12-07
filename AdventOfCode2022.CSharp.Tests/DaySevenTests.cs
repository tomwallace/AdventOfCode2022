using AdventOfCode2022.CSharp.Seven;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySevenTests
{
    [Fact]
    public void CountOverlapSections()
    {
        string filePath = @"Seven\DaySevenTestInputA.txt";
        var sut = new DaySeven();
        var result = sut.CreateDirectoryStructureAndEvaluate(filePath, 100000);

        Assert.Equal(95437, result);
    }

    [Fact]
    public void CreateDirectoryStructureAndFindDelete()
    {
        string filePath = @"Seven\DaySevenTestInputA.txt";
        var sut = new DaySeven();
        var result = sut.CreateDirectoryStructureAndFindDelete(filePath);

        Assert.Equal(24933642, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartA();

        Assert.Equal("1454188", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySeven();
        var result = sut.PartB();

        Assert.Equal("4183246", result);
    }
}