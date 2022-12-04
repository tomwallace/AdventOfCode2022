using AdventOfCode2022.CSharp.Four;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFourTests
{
    [Fact]
    public void CountOverlapSections()
    {
        string filePath = @"Four\DayFourTestInputA.txt";
        var sut = new DayFour();
        var result = sut.CountOverlapSections(filePath, false);

        Assert.Equal(2, result);
    }

    [Fact]
    public void CountOverlapAtAllSections()
    {
        string filePath = @"Four\DayFourTestInputA.txt";
        var sut = new DayFour();
        var result = sut.CountOverlapSections(filePath, true);

        Assert.Equal(4, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartA();

        Assert.Equal("459", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFour();
        var result = sut.PartB();

        Assert.Equal("779", result);
    }
}