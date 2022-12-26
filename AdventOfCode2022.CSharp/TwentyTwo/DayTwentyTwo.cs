using AdventOfCode2022.CSharp.Utility;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

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

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var password = GetPassword(filePath);

        return password.ToString();
    }

    // TODO: Part B
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
        Coords = new Dictionary<string, bool>();

        var lines = FileUtility.ParseFileToList(filePath);

        var reference = new List<List<char>>();

        // NOTE - the input does not pad the right side, so we have to do that
        var maxWidth = lines.Take(lines.Count - 2).Max(l => l.Length);

        for (int y = 0; y < (lines.Count - 2); y++)
        {
            var lineList = new List<char>();

            var lineChar = lines[y].PadRight(maxWidth).ToCharArray().ToList();
            for (int x = 0; x < lineChar.Count; x++)
            {
                var c = lineChar[x];

                lineList.Add(c);

                if (c == '#' || c == '.')
                    Coords.Add(Point.CoordsToString(x + 1, y + 1), c == '.');
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
        var ins2 = lines[^1];
        var insList = new List<string>();
        var matches = Regex.Matches(ins2, @"(\d+)([RL]?)");
        foreach (Match match in matches)
        {
            insList.Add(match.Groups[1].Value);
            if (!string.IsNullOrEmpty(match.Groups[2].Value))
                insList.Add(match.Groups[2].Value);
        }

        Instructions = insList;
    }

    public Dictionary<int, (int, int)> RowMask { get; set; }

    public Dictionary<int, (int, int)> ColMask { get; set; }

    // True indicates if it is able to be traveled
    public Dictionary<string, bool> Coords { get; set; }

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
                (int x, int y) = Current.Move(RowMask, ColMask);

                if (Coords[Point.CoordsToString(x, y)])
                {
                    Current.X = x;
                    Current.Y = y;
                }
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

    // 0 = right, 1 = down, 2 = left, 3 = up
    public (int, int) Move(Dictionary<int, (int, int)> rowMask, Dictionary<int, (int, int)> colMask)
    {
        switch (Dir)
        {
            case 3:
                return (X, WrapY(X, Y - 1, colMask));

            case 0:
                return (WrapX(X + 1, Y, rowMask), Y);

            case 1:
                return (X, WrapY(X, Y + 1, colMask));

            case 2:
                return (WrapX(X - 1, Y, rowMask), Y);

            default:
                throw new ArgumentException($"Do not recongnize Dir {Dir}");
        }
    }

    public int GetPassword()
    {
        return (1000 * Y) + (4 * X) + Dir;
    }

    private int WrapX(int x, int y, Dictionary<int, (int, int)> rowMask)
    {
        if (x > rowMask[y].Item2)
            return rowMask[y].Item1;
        if (x < rowMask[y].Item1)
            return rowMask[y].Item2;

        return x;
    }

    private int WrapY(int x, int y, Dictionary<int, (int, int)> colMask)
    {
        if (y > colMask[x].Item2)
            return colMask[x].Item1;
        if (y < colMask[x].Item1)
            return colMask[x].Item2;

        return y;
    }
}