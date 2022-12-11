namespace AdventOfCode2022.CSharp.Eleven;

public class Monkey
{
    private long numberInspections;

    public Monkey(string items, int name, Func<long, long> func, int divider, int monkeyTrue, int monkeyFalse)
    {
        Name = name;
        Items = items.Split(',').Select(i => long.Parse(i.Trim())).ToList();
        TestDivider = divider;
        MonkeyIfTrue = monkeyTrue;
        MonkeyIfFalse = monkeyFalse;

        Operation = func;

        numberInspections = 0;
    }

    public List<long> Items { get; set; }

    public int Name { get; }

    // Ex: old => old * 19
    public Func<long, long> Operation { get; }

    public int TestDivider { get; }

    public int MonkeyIfTrue { get; }

    public int MonkeyIfFalse { get; }

    // Mutates monkey dictionary
    public void ExecuteRound(Dictionary<int, Monkey> monkeys, bool manageWorryLevels, int lcm)
    {
        foreach (var item in Items)
        {
            var worryLevel = manageWorryLevels ? (long)(Operation(item) / 3) : (long)Operation(item) % lcm;
            if (worryLevel % TestDivider == 0)
                monkeys[MonkeyIfTrue].Items.Add(worryLevel);
            else
                monkeys[MonkeyIfFalse].Items.Add(worryLevel);

            numberInspections++;
        }

        // All items gone
        Items = new List<long>();
    }

    public long GetNumberInspections() => numberInspections;

    public override string ToString()
    {
        return string.Join(",", Items);
    }
}