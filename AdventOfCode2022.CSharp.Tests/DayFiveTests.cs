using AdventOfCode2022.CSharp.Five;
using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Tests;

public class DayFiveTests
{
    /*
        [D]
    [N] [C]
    [Z] [M] [P]
    1   2   3
     */

    private Dictionary<int, List<char>> testStacks = new Dictionary<int, List<char>>()
    {
        { 1, new List<char>() { 'Z', 'N' } },
        { 2, new List<char>() { 'M', 'C', 'D' } },
        { 3, new List<char>() { 'P' } }
    };

    [Fact]
    public void CountOverlapSections()
    {
        string filePath = @"Five\DayFiveTestInputA.txt";
        var sut = new DayFive();
        var directions = FileUtility.ParseFileToList(filePath, line => line);
        var result = sut.MoveAndGetTopCrates(directions, testStacks, true);

        Assert.Equal("CMZ", result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartA();

        Assert.Equal("RNZLFZSJH", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayFive();
        var result = sut.PartB();

        Assert.Equal("CNSFCGJSM", result);
    }
}