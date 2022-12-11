namespace AdventOfCode2022.CSharp.Eleven;

public class DayEleven : IAdventProblemSet
{
    private Dictionary<int, Monkey> monkeyInput = new Dictionary<int, Monkey>()
    {
        { 0, new Monkey("56, 56, 92, 65, 71, 61, 79", 0, old => old * 7, 3, 3, 7) },
        { 1, new Monkey("61, 85", 1, old => old + 5, 11, 6, 4) },
        { 2, new Monkey("54, 96, 82, 78, 69", 2, old => old * old, 7, 0, 7) },
        { 3, new Monkey("57, 59, 65, 95", 3, old => old + 4, 2, 5, 1) },
        { 4, new Monkey("62, 67, 80", 4, old => old * 17, 19, 2, 6) },
        { 5, new Monkey("91", 5, old => old + 7, 5, 1, 4) },
        { 6, new Monkey("79, 83, 64, 52, 77, 56, 63, 92", 0, old => old + 6, 17, 2, 0) },
        { 7, new Monkey("50, 97, 76, 96, 80, 56", 7, old => old + 3, 13, 3, 5) }
    };

    /// <inheritdoc />
    public string Description()
    {
        return "Monkey in the Middle";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 11;
    }

    /// <inheritdoc />
    public string PartA()
    {
        var level = FindMonkeyBusinessLevel(monkeyInput, 20, true);

        return level.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        var level = FindMonkeyBusinessLevel(monkeyInput, 10000, false);

        return level.ToString();
    }

    public long FindMonkeyBusinessLevel(Dictionary<int, Monkey> monkeys, int iterations, bool manageWorryLevels)
    {
        // If !manageWorryLevels, we can use the remainder from a common divisor (multiply the divisors together) to prevent
        // things from going out of hand.  I got help from Reddit for this concept
        var commonDivisor = monkeys.Select(m => m.Value.TestDivider).Aggregate((a, d) => a * d);
        for (int iter = 1; iter <= iterations; iter++)
        {
            for (int m = 0; m < monkeys.Count; m++)
            {
                monkeys[m].ExecuteRound(monkeys, manageWorryLevels, commonDivisor);
            }
        }

        var sortedMonkeys = monkeys.Select(m => m.Value.GetNumberInspections()).OrderByDescending(m => m).ToList();
        return sortedMonkeys[0] * sortedMonkeys[1];
    }
}