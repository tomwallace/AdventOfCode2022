using AdventOfCode2022.CSharp.Nineteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayNineteenTests
{
    [Fact]
    public void SumQualityLevel()
    {
        string filePath = @"Nineteen\DayNineteenTestInputA.txt";
        var sut = new DayNineteen();
        var result = sut.SumQualityLevel(filePath);

        Assert.Equal(33, result);
    }

    // Too low - 1452 - takes 2.9 min to run
    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartA();

        Assert.Equal("-1", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNineteen();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}