using AdventOfCode2022.CSharp.Utility;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022.CSharp.TwentyThree;

public class Crater
{
    public Crater(string filePath)
    {
        Elves = new Dictionary<string, Elf>();
        var lines = FileUtility.ParseFileToList(filePath);

        var counter = 0;
        for (int y = 0; y < lines.Count; y++)
        {
            var line = lines[y].ToCharArray();
            for (int x = 0; x < line.Length; x++)
            {
                var c = line[x];

                if (c == '#')
                {
                    var elf = new Elf(counter, x, y);
                    Elves.Add(elf.ToString(), elf);
                    counter++;
                }
            }
        }

        MoveRules = new List<Rule>()
        {
            new Rule("North", e => e.CanMoveNorth(Elves)),
            new Rule( "South",e => e.CanMoveSouth(Elves)),
            new Rule("West", e => e.CanMoveWest(Elves)),
            new Rule("East", e => e.CanMoveEast(Elves))
        };
    }

    public Dictionary<string, Elf> Elves { get; set; }

    public List<Rule> MoveRules { get; set; }

    public bool CompleteRound()
    {
        var proposed = new Dictionary<int, (int, int)>();
        var elfMoved = false;

        // Evaluate proposed moves
        foreach (var elf in Elves)
        {
            if (!elf.Value.CanMoveAtAll(Elves))
                continue;

            elfMoved = true;

            foreach (var rule in MoveRules)
            {
                var result = rule.Op.Invoke(elf.Value);
                if (result != null)
                {
                    proposed.Add(elf.Value.Id, result.Value);
                    break;
                }
            }
        }

        // Execute moves, skipping any duplicate values, recreating the Elves dictionary
        var updatedElves = new Dictionary<string, Elf>();
        foreach (var move in proposed)
        {
            if (proposed.Count(p => p.Value == move.Value) > 1)
                continue;

            var elf = Elves.Single(e => e.Value.Id == move.Key);
            var updatedElf = new Elf(elf.Value.Id, move.Value.Item1, move.Value.Item2);
            updatedElves.Add(updatedElf.ToString(), updatedElf);
        }

        // Add back any who remained stationary
        var updatedIds = updatedElves.Select(u => u.Value.Id);
        var stationaryElves = Elves.Where(e => !updatedIds.Contains(e.Value.Id));
        foreach (var elf in stationaryElves)
        {
            updatedElves.Add(elf.Key, elf.Value);
        }

        Elves = updatedElves;

        // Reorder rules
        var first = MoveRules[0];
        MoveRules.RemoveAt(0);
        MoveRules.Insert(3, first);

        return elfMoved;
    }

    public ((int, int), (int, int)) FindRange()
    {
        var minX = Elves.Min(e => e.Value.X);
        var maxX = Elves.Max(e => e.Value.X);
        var minY = Elves.Min(e => e.Value.Y);
        var maxY = Elves.Max(e => e.Value.Y);

        return ((minX, maxX), (minY, maxY));
    }

    public int CountEmptyInRange()
    {
        var range = FindRange();
        var totalArea = (range.Item1.Item2 - range.Item1.Item1 + 1) * (range.Item2.Item2 - range.Item2.Item1 + 1);

        return totalArea - Elves.Count;
    }

    public void Print()
    {
        var range = FindRange();
        for (int y = range.Item2.Item1; y <= range.Item2.Item2; y++)
        {
            var builder = new StringBuilder();
            for (int x = range.Item1.Item1; x <= range.Item1.Item2; x++)
            {
                if (Elves.ContainsKey(Elf.CoordToString(x, y)))
                    builder.Append("#");
                else
                    builder.Append(".");
            }

            Debug.WriteLine(builder.ToString());
        }
        Debug.WriteLine("");
        Debug.WriteLine("");
    }
}