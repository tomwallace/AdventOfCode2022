namespace AdventOfCode2022.CSharp.TwentyFour;

public class DayTwentyFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Blizzard Basin [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 24;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyFour\DayTwentyFourInput.txt";
        var count = MinutesUntilExit(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyFour\DayTwentyFourInput.txt";
        var count = MinutesUntilExitThenBack(filePath);

        return count.ToString();
    }

    public int MinutesUntilExit(string filePath)
    {
        var blizzard = new Blizzard(filePath);
        return blizzard.BFS(blizzard.Finish);
    }

    public int MinutesUntilExitThenBack(string filePath)
    {
        var blizzard = new Blizzard(filePath);
        blizzard.BFS(blizzard.Finish);
        blizzard.BFS(blizzard.Start);
        return blizzard.BFS(blizzard.Finish);
    }
}