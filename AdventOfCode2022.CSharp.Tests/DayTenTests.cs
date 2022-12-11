using AdventOfCode2022.CSharp.Ten;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTenTests
{
    [Fact]
    public void MoveHeadAndCount()
    {
        string filePath = @"Ten\DayTenTestInputA.txt";
        var sut = new DayTen();
        var result = sut.ProcessInstructions(filePath, false);

        Assert.Equal(13140, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartA();

        Assert.Equal("13680", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTen();
        var result = sut.PartB();

        Assert.Equal("PZGPKPEB", result);
    }
}