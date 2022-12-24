namespace AdventOfCode2022.CSharp.TwentyThree;

public class Elf
{
    public Elf(int id, int x, int y)
    {
        Id = id;
        X = x;
        Y = y;
    }

    public int Id { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public override string ToString()
    {
        return CoordToString(X, Y);
    }

    public static string CoordToString(int x, int y)
    {
        return $"{x},{y}";
    }

    public bool CanMoveAtAll(Dictionary<string, Elf> elves)
    {
        var pos = new[] { (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0) };
        foreach (var p in pos)
        {
            if (elves.ContainsKey(CoordToString(X + p.Item1, Y + p.Item2)))
                return true;
        }

        return false;
    }

    public (int, int)? CanMoveNorth(Dictionary<string, Elf> elves)
    {
        var pos = new[] { (-1, -1), (0, -1), (1, -1) };
        foreach (var p in pos)
        {
            if (elves.ContainsKey(CoordToString(X + p.Item1, Y + p.Item2)))
                return null;
        }

        return (X, Y - 1);
    }

    public (int, int)? CanMoveEast(Dictionary<string, Elf> elves)
    {
        var pos = new[] { (1, -1), (1, 0), (1, 1) };
        foreach (var p in pos)
        {
            if (elves.ContainsKey(CoordToString(X + p.Item1, Y + p.Item2)))
                return null;
        }

        return (X + 1, Y);
    }

    public (int, int)? CanMoveSouth(Dictionary<string, Elf> elves)
    {
        var pos = new[] { (-1, 1), (0, 1), (1, 1) };
        foreach (var p in pos)
        {
            if (elves.ContainsKey(CoordToString(X + p.Item1, Y + p.Item2)))
                return null;
        }

        return (X, Y + 1);
    }

    public (int, int)? CanMoveWest(Dictionary<string, Elf> elves)
    {
        var pos = new[] { (-1, -1), (-1, 0), (-1, 1) };
        foreach (var p in pos)
        {
            if (elves.ContainsKey(CoordToString(X + p.Item1, Y + p.Item2)))
                return null;
        }

        return (X - 1, Y);
    }
}