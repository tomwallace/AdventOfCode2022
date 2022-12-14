using AdventOfCode2022.CSharp.Fourteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFourteenTests
{
    [Fact]
    public void CalculateAmountSandBeforeDone()
    {
        string filePath = @"Fourteen\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.CalculateAmountSandBeforeDone(filePath, false);

        Assert.Equal(24, result);
    }

    [Fact]
    public void CalculateAmountSandBeforeDone_HasFloor()
    {
        string filePath = @"Fourteen\DayFourteenTestInputA.txt";
        var sut = new DayFourteen();
        var result = sut.CalculateAmountSandBeforeDone(filePath, true);

        Assert.Equal(93, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartA();

        Assert.Equal("779", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFourteen();
        var result = sut.PartB();

        Assert.Equal("27426", result);
    }
}