using AdventOfCode2022.CSharp.Utility;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.CSharp.TwentyTwo;

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

    public void FollowInstruction(string ins, IEdgeMapping mapping)
    {
        if (ins == "L" || ins == "R")
            Current.Rotate(ins);
        else
        {
            var steps = int.Parse(ins);
            for (int i = 0; i < steps; i++)
            {
                (int x, int y, int dir) = Current.Move(mapping);

                if (Coords[Point.CoordsToString(x, y)])
                {
                    Current.X = x;
                    Current.Y = y;
                    Current.Dir = dir;
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