namespace AdventOfCode2022.CSharp.Eighteen;

public class Cube
{
    private readonly List<(int, int, int)> mods = new List<(int, int, int)>()
    {
        (0, 0, 1), (0, 0, -1), (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0)
    };

    // Assumes North = Y + 1, East = X + 1
    public Cube(string input)
    {
        var split = input.Split(',');
        X = int.Parse(split[0]);
        Y = int.Parse(split[1]);
        Z = int.Parse(split[2]);
    }

    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public int FindOpenSides(Dictionary<string, Cube> cubes)
    {
        var result = mods.Select(i =>
        {
            var compareKey = $"{X + i.Item1},{Y + i.Item2},{Z + i.Item3}";
            if (!cubes.ContainsKey(compareKey))
                return 1;
            return 0;
        }).Sum();

        return result;
    }

    public List<Cube> GetNeighbors()
    {
        return mods.Select(i => new Cube(X + i.Item1, Y + i.Item2, Z + i.Item3)).ToList();
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}