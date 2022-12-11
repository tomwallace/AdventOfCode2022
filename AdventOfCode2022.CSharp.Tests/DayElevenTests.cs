using AdventOfCode2022.CSharp.Eleven;

namespace AdventOfCode2022.CSharp.Tests;

public class DayElevenTests
{
    private Dictionary<int, Monkey> monkeys = new Dictionary<int, Monkey>()
    {
        { 0, new Monkey("79,98", 0, old => old * 19, 23, 2, 3) },
        { 1, new Monkey("54,65,75,74", 1, old => old + 6, 19, 2, 0) },
        { 2, new Monkey("79,60,97", 2, old => old * old, 13, 1, 3) },
        { 3, new Monkey("74", 3, old => old + 3, 17, 0, 1) }
    };

    [Fact]
    public void FindMonkeyBusinessLevel()
    {
        var sut = new DayEleven();
        var result = sut.FindMonkeyBusinessLevel(monkeys, 20, true);

        Assert.Equal(10605, result);
    }

    [Fact]
    public void FindMonkeyBusinessLevel_NotManagingWorryLevels()
    {
        var sut = new DayEleven();
        var result = sut.FindMonkeyBusinessLevel(monkeys, 10000, false);

        Assert.Equal(2713310158, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartA();

        Assert.Equal("67830", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayEleven();
        var result = sut.PartB();

        Assert.Equal("15305381442", result);
    }
}