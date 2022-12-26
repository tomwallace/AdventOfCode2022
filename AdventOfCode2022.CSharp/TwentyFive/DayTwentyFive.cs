using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.TwentyFive;

public class DayTwentyFive : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Full of Hot Air";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 25;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyFive\DayTwentyFiveInput.txt";
        var snafuNumber = FindSnafuNumberFromSumOfFuel(filePath);

        return snafuNumber;
    }

    /// <inheritdoc />
    public string PartB()
    {
        // There is no Part B for Day 25.  You get it by completing all of the other puzzles.
        return "Got all the stars!";
    }

    public string FindSnafuNumberFromSumOfFuel(string filePath)
    {
        var snafuNumbers = FileUtility.ParseFileToList(filePath, line => new SnafuNumber(line));
        var sum = snafuNumbers.Sum(s => s.ToDecimal());
        return new SnafuNumber(sum).ToString();
    }
}