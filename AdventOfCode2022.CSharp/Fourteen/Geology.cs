using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Fourteen;

public class Geology
{
    private readonly int maxY;

    public Geology(string filePath)
    {
        Points = new Dictionary<string, char>();
        maxY = 0;

        var instructions = FileUtility.ParseFileToList(filePath);
        foreach (var inst in instructions)
        {
            var splits = inst.Split(new string[] { " -> " }, StringSplitOptions.TrimEntries);
            var splitsPairs = splits.Pairwise();

            foreach (var pair in splitsPairs)
            {
                var firstSplit = pair.Item1.Split(',');
                var secondSplit = pair.Item2.Split(',');

                var xs = new List<int>() { int.Parse(firstSplit[0]), int.Parse(secondSplit[0]) };
                xs.Sort();
                var ys = new List<int>() { int.Parse(firstSplit[1]), int.Parse(secondSplit[1]) };
                ys.Sort();

                for (int x = xs[0]; x <= xs[1]; x++)
                {
                    for (int y = ys[0]; y <= ys[1]; y++)
                    {
                        if (!Points.ContainsKey($"{x},{y}"))
                        {
                            Points.Add($"{x},{y}", '#');

                            if (y > maxY)
                                maxY = y;
                        }
                    }
                }
            }
        }

        // "Floor" is defined as 2 below lowest point
        maxY += 2;
    }

    public Dictionary<string, char> Points { get; set; }

    // Returns true if sand falls past the maxY point
    public bool AddGrainSand(int sandX, int sandY)
    {
        var done = false;
        var x = sandX;
        var y = sandY;

        while (!done)
        {
            // Check if past maxY, meaning we are done
            if (y > maxY)
                return true;

            // Check down first
            if (!Points.ContainsKey($"{x},{y + 1}"))
            {
                y++;
                continue;
            }

            // Check down and left
            if (!Points.ContainsKey($"{x - 1},{y + 1}"))
            {
                y++;
                x--;
                continue;
            }

            // Check down and right
            if (!Points.ContainsKey($"{x + 1},{y + 1}"))
            {
                y++;
                x++;
                continue;
            }

            // Otherwise, sand has stopped
            Points.Add($"{x},{y}", 'o');
            done = true;
        }

        return false;
    }

    // Returns true when we record an entry that is sandX and sandY
    // Also treats maxY as a "floor" instead
    public bool AddGrainSandWithFloor(int sandX, int sandY)
    {
        var done = false;
        var x = sandX;
        var y = sandY;

        while (!done)
        {
            // Check down first
            if (!Points.ContainsKey($"{x},{y + 1}") && (y + 1) < maxY)
            {
                y++;
                continue;
            }

            // Check down and left
            if (!Points.ContainsKey($"{x - 1},{y + 1}") && (y + 1) < maxY)
            {
                y++;
                x--;
                continue;
            }

            // Check down and right
            if (!Points.ContainsKey($"{x + 1},{y + 1}") && (y + 1) < maxY)
            {
                y++;
                x++;
                continue;
            }

            // If we would be adding a point that is sandX and sandY, return success
            if (x == sandX && y == sandY)
                return true;

            // Otherwise, sand has stopped
            Points.Add($"{x},{y}", 'o');
            done = true;
        }

        return false;
    }
}