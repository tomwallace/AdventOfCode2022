using AdventOfCode2022.CSharp.Thirteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayThirteenTests
{
    [Theory]
    [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]", true)]
    [InlineData("[[1],[2,3,4]]", "[[1],4]", true)]
    [InlineData("[9]", "[[8,7,6]]", false)]
    [InlineData("[[4,4],4,4]", "[[4,4],4,4,4]", true)]
    [InlineData("[7,7,7,7]", "[7,7,7]", false)]
    [InlineData("[]", "[3]", true)]
    [InlineData("[[[]]]", "[[]]", false)]
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
    public void IsPacketInRightOrderParse(string one, string two, bool expected)
    {
        var sut = new DayThirteen();
        var result = sut.IsPacketInRightOrderParse(one, two);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void CountCorrectPackets()
    {
        string filePath = @"Thirteen\DayThirteenTestInputA.txt";
        var sut = new DayThirteen();
        var result = sut.CountCorrectPackets(filePath);

        Assert.Equal(13, result);
    }

    [Fact]
    public void SortPackets()
    {
        string filePath = @"Thirteen\DayThirteenTestInputA.txt";
        var sut = new DayThirteen();
        var result = sut.SortPackets(filePath);

        Assert.Equal(140, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartA();

        Assert.Equal("5623", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayThirteen();
        var result = sut.PartB();

        Assert.Equal("20570", result);
    }
}