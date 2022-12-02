using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.One;

public class DayOne : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Calorie Counting";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 1;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"One\DayOneInput.txt";
        List<string> data = FileUtility.ParseFileToList(filePath, line => line);
        var elves = CalculateCalorieTotals(data);

        return elves.Max().ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"One\DayOneInput.txt";
        List<string> data = FileUtility.ParseFileToList(filePath, line => line);
        var total = TotalCaloriesTopThreeElves(data);

        return total.ToString();
    }

    public List<int> CalculateCalorieTotals(List<string> data)
    {
        var elves = new List<int>();
        var currentElf = 0;
        foreach (var line in data)
        {
            if (string.IsNullOrEmpty(line))
            {
                elves.Add(currentElf);
                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(line.Trim());
            }
        }

        // Last line
        elves.Add(currentElf);

        return elves;
    }

    public int TotalCaloriesTopThreeElves(List<string> data)
    {
        var elves = CalculateCalorieTotals(data);
        var topThree = elves.OrderByDescending(e => e).Take(3);
        return topThree.Sum();
    }
}