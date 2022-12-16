using AdventOfCode2022.CSharp.Fifteen;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFifteenTests
{
    [Fact]
    public void CountRowLocationsNoBeacon()
    {
        string filePath = @"Fifteen\DayFifteenTestInputA.txt";
        var sut = new DayFifteen();
        var result = sut.CountRowLocationsNoBeacon(filePath, 10);

        Assert.Equal(26, result);
    }

    [Fact]
    public void FindTuningFrequencyOfBeacon()
    {
        string filePath = @"Fifteen\DayFifteenTestInputA.txt";
        var sut = new DayFifteen();
        var result = sut.FindTuningFrequencyOfBeacon(filePath, 0, 20);

        Assert.Equal(56000011, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartA();

        Assert.Equal("5511201", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFifteen();
        var result = sut.PartB();

        Assert.Equal("11318723411840", result);
    }
}