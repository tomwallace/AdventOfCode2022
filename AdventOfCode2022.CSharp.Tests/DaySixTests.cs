using AdventOfCode2022.CSharp.Six;

namespace AdventOfCode2022.CSharp.Tests;

public class DaySixTests
{
    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4, 7)]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, 19)]
    public void CountOverlapSections(string input, int numberOfChars, int expected)
    {
        var sut = new DaySix();
        var result = sut.FindRepeatMarker(input, numberOfChars);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartA();

        Assert.Equal("1282", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DaySix();
        var result = sut.PartB();

        Assert.Equal("3513", result);
    }
}