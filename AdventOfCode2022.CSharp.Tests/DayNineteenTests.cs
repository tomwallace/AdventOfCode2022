using AdventOfCode2022.CSharp.Nineteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayNineteenTests
{
    [Fact]
    public void SumQualityLevelPriorityQueue()
    {
        string filePath = @"Nineteen\DayNineteenTestInputA.txt";
        var sut = new DayNineteen();
        var result = sut.SumQualityLevelPriorityQueue(filePath, 24);

        Assert.Equal(33, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartA();

        Assert.Equal("1528", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartB();

        Assert.Equal("16926", result);
    }
}