using AdventOfCode2022.CSharp.Twenty;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyTests
{
    [Fact]
    public void SumGroveCoordinatesUsingLinkedList()
    {
        string filePath = @"Twenty\DayTwentyTestInputA.txt";
        var sut = new DayTwenty();
        var result = sut.SumGroveCoordinatesUsingLinkedList(filePath);

        Assert.Equal(3, result);
    }

    [Fact]
    public void SumGroveCoordinatesUsingLinkedList_WithDecryptionKey()
    {
        string filePath = @"Twenty\DayTwentyTestInputA.txt";
        var sut = new DayTwenty();
        var result = sut.SumGroveCoordinatesUsingLinkedList(filePath, 811589153, 10);

        Assert.Equal(1623178306, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartA();

        Assert.Equal("4151", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwenty();
        var result = sut.PartB();

        Assert.Equal("7848878698663", result);
    }
}