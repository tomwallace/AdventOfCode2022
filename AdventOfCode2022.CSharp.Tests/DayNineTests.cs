using AdventOfCode2022.CSharp.Nine;

namespace AdventOfCode2022.CSharp.Tests;

public class DayNineTests
{
    [Fact]
    public void MoveHeadAndCount()
    {
        string filePath = @"Nine\DayNineTestInputA.txt";
        var sut = new DayNine();
        var result = sut.MoveHeadAndCount(filePath, 2);

        Assert.Equal(13, result);
    }

    [Theory]
    [InlineData(@"Nine\DayNineTestInputA.txt", 1)]
    [InlineData(@"Nine\DayNineTestInputB.txt", 36)]
    public void MoveHeadAndCount_Many(string filePath, int expected)
    {
        var sut = new DayNine();
        var result = sut.MoveHeadAndCount(filePath, 10);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartA();

        Assert.Equal("6503", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayNine();
        var result = sut.PartB();

        Assert.Equal("2724", result);
    }
}