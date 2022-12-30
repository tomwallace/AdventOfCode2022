namespace AdventOfCode2022.CSharp.TwentyTwo;

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
    public (int, int, int) Move(IEdgeMapping mapping)
    {
        return mapping.Map(X, Y, Dir);
    }

    public int GetPassword()
    {
        return (1000 * Y) + (4 * X) + Dir;
    }
}