using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Nine;

public class DayNine : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Rope Bridge";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 9;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Nine\DayNineInput.txt";
        var count = MoveHeadAndCount(filePath, 2);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Nine\DayNineInput.txt";
        var count = MoveHeadAndCount(filePath, 10);

        return count.ToString();
    }

    public int MoveHeadAndCount(string filePath, int numberOfKnots)
    {
        var knots = new List<Knot>();
        for (int i = 0; i < numberOfKnots; i++)
        {
            knots.Add(new Knot(0, 0));
        }

        var tailLocations = new HashSet<string>();
        tailLocations.Add(knots.Last().ToString());

        var moves = FileUtility.ParseFileToList(filePath);
        foreach (var move in moves)
        {
            var splits = move.Split(' ');
            var dir = splits[0][0];
            var times = int.Parse(splits[1]);
            for (int i = 0; i < times; i++)
            {
                knots.First().Move(dir);
                for (int k = 1; k < knots.Count; k++)
                {
                    knots[k].Follow(knots[k - 1]);
                }

                tailLocations.Add(knots.Last().ToString());
            }
        }

        return tailLocations.Count;
    }
}