using System.Diagnostics;
using System.Text;
using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.TwentyTwo;

public class DayTwentyTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Monkey Map";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 22;
    }

    // TODO: Come back to because works for sample, but not for Part A
    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var password = GetPassword(filePath);

        return password.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        //var val = FindValueOfRoot(filePath);

        return "";
    }

    public int GetPassword(string filePath)
    {
        var map = new Map(filePath);
        foreach (var ins in map.Instructions)
        {
            map.FollowInstruction(ins);
            //map.Print();
        }

        return map.Current.GetPassword();
    }
}

public class Map
{
    public Map(string filePath)
    {
        RowMask = new Dictionary<int, (int, int)>();
        ColMask = new Dictionary<int, (int, int)>();
        Walls = new Dictionary<string, char>();
        
        var lines = FileUtility.ParseFileToList(filePath);

        var reference = new List<List<char>>();

        // NOTE - the input does not pad the right side, so we have to do that
        var maxWidth = lines.Max(l => l.Length);

        for (int y = 0; y < (lines.Count - 2); y++)
        {
            var lineList = new List<char>();

            var lineChar = lines[y].PadRight(maxWidth).ToCharArray().ToList();
            for (int x = 0; x < lineChar.Count; x++)
            {
                var c = lineChar[x];

                lineList.Add(c);

                if (c == '#')
                    Walls.Add(Point.CoordsToString(x + 1, y + 1), '#');
            }

            var firstCharIndex = lineChar.FindIndex(c => c != ' ');
            var lastCharIndex = lineChar.FindLastIndex(c => c != ' ');
            RowMask.Add(y + 1, (firstCharIndex + 1, lastCharIndex + 1));

            reference.Add(lineList);
        }

        // Create colMask
        for (int x = 0; x < reference[0].Count; x++)
        {
            var col = reference.Select(c => c[x]).ToList();
            
            var firstCharIndex = col.FindIndex(c => c != ' ');
            var lastCharIndex = col.FindLastIndex(c => c != ' ');
            ColMask.Add(x + 1, (firstCharIndex + 1, lastCharIndex + 1));
        }

        Current = new Point(RowMask[1].Item1, 1, 0);
        Reference = reference;

        // Handle instructions
        var ins = lines[^1].ToCharArray();
        var insList = new List<string>();

        var numberCollector = "";
        for (int i = 0; i < ins.Length; i++)
        {
            if (ins[i] == 'L' || ins[i] == 'R')
            {
                insList.Add(numberCollector);
                insList.Add(ins[i].ToString());
                numberCollector = "";
            }
            else
            {
                numberCollector = $"{numberCollector}{ins[i]}";
            }
        }

        if (!string.IsNullOrEmpty(numberCollector))
            insList.Add(numberCollector);

        Instructions = insList;
    }
    
    public Dictionary<int, (int, int)> RowMask { get; set; }

    public Dictionary<int, (int, int)> ColMask { get; set; }

    public Dictionary<string, char> Walls { get; set; }

    public Point Current { get; set; }

    // Y,X
    // Starts at 1, 1
    public List<List<char>> Reference { get; set; }

    public List<string> Instructions { get; set; }

    public void FollowInstruction(string ins)
    {
        if (ins == "L" || ins == "R")
            Current.Rotate(ins);
        else
        {
            var steps = int.Parse(ins);
            for (int i = 0; i < steps; i++)
            {
                (int x, int y) = Current.Move();
                // Would hit wall
                if (Walls.ContainsKey(Point.CoordsToString(x, y)))
                    return;

                // Otherwise move
                Current.X = WrapX(x, Current.Y);
                Current.Y = WrapY(Current.X, y);
            }
        }
    }

    public void Print()
    {
        for (int y = 0; y < Reference.Count; y++)
        {
            var builder = new StringBuilder();
            for (int x = 0; x < Reference[0].Count; x++)
            {
                if (Current.X == x + 1 && Current.Y == y + 1)
                {
                    builder.Append(Current.DirToString());
                }
                else
                {
                    builder.Append(Reference[y][x]);
                }
            }    

            Debug.WriteLine(builder.ToString());
        }
        Debug.WriteLine("");
        Debug.WriteLine("");
    }

    private int WrapX(int x, int y)
    {
        if (x > RowMask[y].Item2)
            return RowMask[y].Item1;
        if (x < RowMask[y].Item1)
            return RowMask[y].Item2;

        return x;
    }

    private int WrapY(int x, int y)
    {
        if (y > ColMask[x].Item2)
            return ColMask[x].Item1;
        if (y < ColMask[x].Item1)
            return ColMask[x].Item2;

        return y;
    }
}

public class Point
{
    public Point(int x, int y, int dir)
    {
        X = x;
        Y = y;
        Dir = dir;
    }

    public int X { get; set; }

    public int Y { get; set; }

    // 0 = right, 1 = down, 2 = left, 3 = up
    public int Dir { get; set; }

    public string DirToString()
    {
        switch (Dir)
        {
            case 3:
                return "^";
            case 0:
                return ">";
            case 1:
                return "v";
            case 2:
                return "<";
            default:
                throw new ArgumentException($"Do not recongnize Dir {Dir}");
        }
    }

    public override string ToString()
    {
        return $"{X},{Y},{DirToString()}";
    }

    public static string CoordsToString(int x, int y)
    {
        return $"{x},{y}";
    }

    public void Rotate(string ins)
    {
        switch (ins)
        {
            case "R":
                Dir++;
                if (Dir > 3)
                    Dir = 0;
                break;
            case "L":
                Dir--;
                if (Dir < 0)
                    Dir = 3;
                break;
            default:
                throw new ArgumentException($"Do not recognize rotate command {ins}");
        }
    }

    public (int, int) Move()
    {
        switch (Dir)
        {
            case 3:
                return (X, Y - 1);
            case 0:
                return (X + 1, Y);
            case 1:
                return (X, Y + 1);
            case 2:
                return (X - 1, Y);
            default:
                throw new ArgumentException($"Do not recongnize Dir {Dir}");
        }
    }

    public int GetPassword()
    {
        return (1000 * Y) + (4 * X) + Dir;
    }
}