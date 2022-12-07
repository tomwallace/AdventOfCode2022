using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Four;

public class DayFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Camp Cleanup";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 4;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Four\DayFourInput.txt";
        var count = CountOverlapSections(filePath, false);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Four\DayFourInput.txt";
        var count = CountOverlapSections(filePath, true);

        return count.ToString();
    }

    public int CountOverlapSections(string filePath, bool atAll)
    {
        var countOverlapped = 0;
        var lines = FileUtility.ParseFileToList(filePath);
        foreach (var line in lines)
        {
            var split = line.Split(',');
            var first = new Range(split[0]);
            var second = new Range(split[1]);

            if (!atAll && FullyContained(first, second))
                countOverlapped++;

            if (atAll && ContainedAtAll(first, second))
                countOverlapped++;
        }

        return countOverlapped;
    }

    private bool FullyContained(Range first, Range second)
    {
        if (second.Min >= first.Min && second.Max <= first.Max)
            return true;

        if (first.Min >= second.Min && first.Max <= second.Max)
            return true;

        return false;
    }

    private bool ContainedAtAll(Range first, Range second)
    {
        var intersection = first.Actual.Intersect(second.Actual);

        return intersection.Any();
    }
}