using AdventOfCode2022.CSharp.TwentyFive;
using AdventOfCode2022.CSharp.TwentyThree;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyFiveTests
{
    [Theory]
    [InlineData("2=-01", 976)]
    [InlineData("1=-0-2", 1747)]
    [InlineData("12111", 906)]
    [InlineData("2=0=", 198)]
    [InlineData("21", 11)]
    [InlineData("2=01", 201)]
    [InlineData("111", 31)]
    [InlineData("20012", 1257)]
    [InlineData("112", 32)]
    [InlineData("1=-1=", 353)]
    [InlineData("1-12", 107)]
    [InlineData("12", 7)]
    [InlineData("1=", 3)]
    [InlineData("122", 37)]
    public void SnafuNumber_ToDecimal(string input, long expected)
    {
        var sut = new SnafuNumber(input);
        var result = sut.ToDecimal();

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "1=")]
    [InlineData(4, "1-")]
    [InlineData(5, "10")]
    [InlineData(6, "11")]
    [InlineData(7, "12")]
    [InlineData(8, "2=")]
    [InlineData(9, "2-")]
    [InlineData(10, "20")]
    [InlineData(15, "1=0")]
    [InlineData(20, "1-0")]
    [InlineData(2022, "1=11-2")]
    [InlineData(12345, "1-0---0")]
    [InlineData(314159265, "1121-1110-1=0")]
    public void SnafuNumber_FromDecimal(long input, string expected)
    {
        var sut = new SnafuNumber(input);

        Assert.Equal(expected, sut.ToString());
    }

    [Fact]
    public void FindSnafuNumberFromSumOfFuel()
    {
        string filePath = @"TwentyFive\DayTwentyFiveTestInputA.txt";
        var sut = new DayTwentyFive();
        var result = sut.FindSnafuNumberFromSumOfFuel(filePath);

        Assert.Equal("2=-1=0", result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartA();

        Assert.Equal("122-12==0-01=00-0=02", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyFive();
        var result = sut.PartB();

        Assert.Equal("Got all the stars!", result);
    }
}