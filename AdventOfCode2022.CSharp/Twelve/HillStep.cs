namespace AdventOfCode2022.CSharp.Twelve;

public class HillStep
{
    public HillStep(int x, int y, int height, HillStep parent)
    {
        X = x;
        Y = y;
        Height = height;
        var hash = parent.PreviousSteps.ToList().ToHashSet();
        hash.Add($"{X},{Y}");
        PreviousSteps = hash;
    }

    public HillStep(int x, int y, int height)
    {
        X = x;
        Y = y;
        Height = height;
        PreviousSteps = new HashSet<string>() { $"{X},{Y}" };
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int Height { get; set; }

    public HashSet<string> PreviousSteps { get; set; }

    public List<HillStep> NextSteps(List<List<char>> map)
    {
        var nextSteps = new List<HillStep>();

        var possible = new List<(int, int)> { (X, Y - 1), (X + 1, Y), (X, Y + 1), (X - 1, Y) };
        foreach (var coord in possible)
        {
            // Don't process past boundaries of map
            if (coord.Item1 < 0 || coord.Item1 >= map[0].Count || coord.Item2 < 0 || coord.Item2 >= map.Count)
                continue;

            var targetHeight = (int)map[coord.Item2][coord.Item1];
            // Can't step up too high
            if (targetHeight > (Height + 1))
                continue;

            // Don't want to repeat steps
            if (PreviousSteps.Contains($"{coord.Item1},{coord.Item2}"))
                continue;

            var newStep = new HillStep(coord.Item1, coord.Item2, targetHeight, this);
            nextSteps.Add(newStep);
        }

        return nextSteps;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }

    public override bool Equals(object obj)
    {
        HillStepComparer comparer = new HillStepComparer();
        return comparer.Equals(this, (HillStep)obj);
    }

    public override int GetHashCode()
    {
        HillStepComparer comparer = new HillStepComparer();
        return comparer.GetHashCode(this);
    }
}