using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Eighteen;

public class DayEighteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Boiling Boulders";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 18;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        var count = CountOpenSides(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Eighteen\DayEighteenInput.txt";
        var count = CountOutsideSidesWithFlood(filePath);

        return count.ToString();
    }

    public int CountOpenSides(string filePath)
    {
        var cubes = FileUtility.ParseFileToDictionary(filePath, (line => line, line => new Cube(line)));
        var result = cubes.Select(c => c.Value.FindOpenSides(cubes)).Sum();

        return result;
    }

    // Got stuck with different methods of counting outsides and insides based on scaling each side out in its cardinal direction.
    // Suggestion on Reddit to do a flood fill worked.
    public int CountOutsideSidesWithFlood(string filePath)
    {
        var cubes = FileUtility.ParseFileToDictionary(filePath, (line => line, line => new Cube(line)));
        var minX = cubes.Min(c => c.Value.X);
        var minY = cubes.Min(c => c.Value.Y);
        var minZ = cubes.Min(c => c.Value.Z);
        var maxX = cubes.Max(c => c.Value.X);
        var maxY = cubes.Max(c => c.Value.Y);
        var maxZ = cubes.Max(c => c.Value.Z);

        var xRange = Enumerable.Range(minX, maxX + 1).ToList();
        var yRange = Enumerable.Range(minY, maxY + 1).ToList();
        var zRange = Enumerable.Range(minZ, maxZ + 1).ToList();

        var outside = cubes.SelectMany(c => c.Value.GetNeighbors())
            .Where(c => IsOutsideViaFlood(c, cubes, xRange, yRange, zRange));

        return outside.Count();
    }

    // Check by "flooding" the remaining space until we hit the outside of the "box" defined by the ranges.
    // Or, if we don't then it is an inside cube
    private bool IsOutsideViaFlood(Cube cube, Dictionary<string, Cube> cubes, List<int> xRange, List<int> yRange, List<int> zRange)
    {
        // Escape if the cube is already in our initial set
        if (cubes.ContainsKey(cube.ToString()))
            return false;

        var checkedCubes = new HashSet<string>();

        var queue = new Queue<Cube>();
        queue.Enqueue(cube);

        while (queue.Any())
        {
            var currentCube = queue.Dequeue();

            if (checkedCubes.Contains(currentCube.ToString()))
                continue;
            checkedCubes.Add(currentCube.ToString());

            // If we hit the outside, then we are good
            if (!xRange.Contains(currentCube.X) || !yRange.Contains(currentCube.Y) || !zRange.Contains(currentCube.Z))
                return true;

            // Otherwise, add the neighbors, so long as they are not in our original list of cubes
            if (!cubes.ContainsKey(currentCube.ToString()))
            {
                foreach (var neighbor in currentCube.GetNeighbors())
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Never found the outside, so must be on the inside
        return false;
    }
}