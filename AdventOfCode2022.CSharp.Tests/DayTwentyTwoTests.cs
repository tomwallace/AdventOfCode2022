using AdventOfCode2022.CSharp.TwentyTwo;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyTwoTests
{
    [Fact]
    public void MakeMap()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoTestInputA.txt";
        var sut = new Map(filePath);

        Assert.Equal(12, sut.Reference.Count);
        Assert.Equal(16, sut.Reference[0].Count);
        Assert.Equal(9, sut.RowMask[1].Item1);
        Assert.Equal(12, sut.RowMask[1].Item2);
        Assert.Equal(5, sut.ColMask[1].Item1);
        Assert.Equal(8, sut.ColMask[1].Item2);
        Assert.Equal(13, sut.Coords.Count(c => c.Value == false));
        Assert.NotNull(sut.Instructions);
        Assert.Equal(13, sut.Instructions.Count);
    }

    [Fact]
    public void GetPassword()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoTestInputA.txt";
        var sut = new DayTwentyTwo();
        var result = sut.GetPassword(filePath);

        Assert.Equal(6032, result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartA();

        Assert.Equal("27436", result);
    }

    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyTwo();
        var result = sut.PartB();

        Assert.Equal("-1", result);
    }
}