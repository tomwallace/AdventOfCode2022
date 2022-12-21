using AdventOfCode2022.CSharp.TwentyOne;

namespace AdventOfCode2022.CSharp.Tests;

public class DayTwentyOneTests
{
    [Fact]
    public void FindValueOfRoot()
    {
        string filePath = @"TwentyOne\DayTwentyOneTestInputA.txt";
        var sut = new DayTwentyOne();
        var result = sut.FindValueOfRoot(filePath);

        Assert.Equal(152, result);
    }

    [Fact]
    public void GetReducedEquation()
    {
        string filePath = @"TwentyOne\DayTwentyOneTestInputA.txt";
        var sut = new DayTwentyOne();
        var result = sut.GetReducedEquation(filePath);

        Assert.Equal("((4+(2*(x-3)))/4) = 150", result);
    }

    [Fact]
    public void PartA_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartA();

        Assert.Equal("70674280581468", result);
    }

    // Evaluates to = 3243420789721, using mathpapa.com
    [Fact]
    public void PartB_Actual()
    {
        var sut = new DayTwentyOne();
        var result = sut.PartB();

        Assert.Equal("((441+((17054834736631-(((4*(((((798+((256+((((2*(((686+((((389+(11*(((104+(((2*(53+((((111+(219+(10*(((((((((999+((3*((2*(((530+(((934+((((6*(((209+(18*(((x-215)/2)+639)))/5)-891))+780)*3)-173))+435)/2))/4)-305))+608))-947))/10)+781)/7)-237)*82)-703)/7)+6))))/5)-188)*2)))-982)/2))/5)-457)))*2)-548)/2))/3)-767))-5)+615)/4))*3))*2)-194)/4)-64))+961)/11))/2))*6) = 19509776378502", result);
    }
}