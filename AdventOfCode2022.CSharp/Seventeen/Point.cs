namespace AdventOfCode2022.CSharp.Seventeen;

public class Point
{
    public Point(long x, long y)
    {
        X = x;
        Y = y;
    }

    public long X { get; set; }

    public long Y { get; set; }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}