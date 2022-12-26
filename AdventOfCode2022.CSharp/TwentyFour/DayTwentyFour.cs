using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.TwentyFour;

public class DayTwentyFour : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Blizzard Basin";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 24;
    }

    // TODO: Still need to complete
    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyFour\DayTwentyFourInput.txt";
        //var count = CountEmptySpace(filePath, 10);

        return "";
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyFour\DayTwentyFourInput.txt";
        //var count = CountEmptySpace(filePath, 10);

        return "";
    }


    public int StepsUntilExit(string filePath)
    {
        //var lines = FileUtility.ParseFileToList(filePath);

        //(Dictionary<string, bool> map, Dictionary<string, int> blizzards) = MakeMap(lines);

        var map = new Map(filePath);

        var start = new Point(1, 0);
        var startState = new MapState(start, new HashSet<string>(), map.StartingBlizzards);
        var minSteps = int.MaxValue;

        var queue = new Queue<MapState>();
        queue.Enqueue(startState);

        while (queue.Any())
        {
            var current = queue.Dequeue();

            // If we are over minSteps, cannot be fewest
            if (current.Steps > minSteps)
                continue;

            if (current.ElfLocation.X == map.TargetX && current.ElfLocation.Y == map.TargetY)
            {
                minSteps = Math.Min(minSteps, current.Steps);
                continue;
            }
                
            // Move blizzards
            var updatedBlizzards = new Dictionary<string, int>();
            foreach (var bliz in current.Blizzards)
            {
                //var blizzard = 
                //updatedBlizzards.Add();
            }

        }

        return minSteps;
    }

    private (Dictionary<string, bool>, Dictionary<string, int>) MakeMap(List<string> lines)
    {
        var map = new Dictionary<string, bool>();
        var blizzards = new Dictionary<string, int>();

        for (int y = 0; y < (lines.Count); y++)
        {
            var lineChar = lines[y].ToCharArray().ToList();
            for (int x = 0; x < lineChar.Count; x++)
            {
                var c = lineChar[x];
                map.Add(Point.CoordsToString(x, y), c != '#');

                if (c != '.' && c != '#')
                {
                    blizzards.Add(Point.CoordsToString(x, y), Point.GetBlizzardFacing(c));
                }
            }
        }

        return (map, blizzards);
    }
}

public class Map
{
    public Map(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath);

        Coordinates = new Dictionary<string, bool>();
        StartingBlizzards = new Dictionary<string, int>();
        RowMask = new Dictionary<int, (int, int)>();
        ColMask = new Dictionary<int, (int, int)>();

        for (int y = 0; y < (lines.Count); y++)
        {
            var lineChar = lines[y].ToCharArray().ToList();
            for (int x = 0; x < lineChar.Count; x++)
            {
                var c = lineChar[x];
                Coordinates.Add(Point.CoordsToString(x, y), c != '#');

                if (c != '.' && c != '#')
                {
                    StartingBlizzards.Add(Point.CoordsToString(x, y), Point.GetBlizzardFacing(c));
                }
            }
            RowMask.Add(y, (1, lines[y].Length - 2));
        }

        // Create colMask
        for (int x = 0; x < lines[0].Length; x++)
        {
            ColMask.Add(x, (1, lines.Count - 2));
        }

        var TargetX = lines[0].Length - 2;
        var TargetY = lines.Count - 1;
    }
    
    public Dictionary<string, bool> Coordinates { get; set; }

    public Dictionary<int, (int, int)> RowMask { get; set; }

    public Dictionary<int, (int, int)> ColMask { get; set; }

    public Dictionary<string, int> StartingBlizzards { get; set; }

    public int TargetX { get; set; }

    public int TargetY { get; set; }
}

public class MapState
{
    public MapState(Point elfLocation, HashSet<string> alreadyBeen, Dictionary<string, int> blizzards)
    {
        ElfLocation = elfLocation;
        AlreadyBeen = alreadyBeen;
        Blizzards = blizzards;
    }

    public Point ElfLocation { get; set; }

    public HashSet<string> AlreadyBeen { get; set; }

    public Dictionary<string, int> Blizzards { get; set; }

    public int Steps => AlreadyBeen.Count + 1;
}

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(int x, int y, int dir)
    {
        X = x;
        Y = y;
        Dir = dir;
    }

    // 0 = North, 1 = East, 2 = South, 3 = West
    public List<(int, int)> Neighbors => new List<(int, int)>()
    {
        (0, -1), (1, 0), (0, 1), (-1, 0)
    }; 

    public int X { get; set; }

    public int Y { get; set; }

    public int? Dir { get; set; }

    public static string CoordsToString(int x, int y)
    {
        return $"{x},{y}";
    }

    public override string ToString()
    {
        return CoordsToString(X, Y);
    }

    public static int GetBlizzardFacing(char blizzard)
    {
        switch (blizzard)
        {
            case '^':
                return 0;
            case '>':
                return 1;
            case 'v':
                return 2;
            case '<':
                return 3;
            default:
                throw new ArgumentException($"Do not recognized Blizzard facing {blizzard}");
        }
    }

    // 0 = right, 1 = down, 2 = left, 3 = up
    public (int, int) MoveBlizzard(Dictionary<int, (int, int)> rowMask, Dictionary<int, (int, int)> colMask)
    {
        switch (Dir)
        {
            case 0:
                return (X, WrapY(Y - 1, colMask));

            case 1:
                return (WrapX(X + 1, rowMask), Y);

            case 2:
                return (X, WrapY(Y + 1, colMask));

            case 3:
                return (WrapX(X - 1, rowMask), Y);

            default:
                throw new ArgumentException($"Do not recongnize Dir {Dir}");
        }
    }

    public List<(int, int)> PotentialMoves()
    {
        // TODO: Start here
        return new List<(int, int)>();
    }

    private int WrapX(int x, Dictionary<int, (int, int)> rowMask)
    {
        if (x > rowMask[Y].Item2)
            return rowMask[Y].Item1;
        if (x < rowMask[Y].Item1)
            return rowMask[Y].Item2;

        return x;
    }

    private int WrapY(int y, Dictionary<int, (int, int)> colMask)
    {
        if (y > colMask[X].Item2)
            return colMask[X].Item1;
        if (y < colMask[X].Item1)
            return colMask[X].Item2;

        return y;
    }
}