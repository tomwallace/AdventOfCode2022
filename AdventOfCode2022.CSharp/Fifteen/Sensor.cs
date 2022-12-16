namespace AdventOfCode2022.CSharp.Fifteen;

public class Sensor
{
    // Ex: Sensor at x=2, y=18: closest beacon is at x=-2, y=15
    public Sensor(string input)
    {
        var splits = input.Split('=');
        var sX = splits[1].Split(',')[0];
        X = int.Parse(sX);

        var sY = splits[2].Split(':')[0];
        Y = int.Parse(sY);

        var bX = splits[3].Split(',')[0];
        var bY = splits[4];

        ClosestBeacon = new Beacon(int.Parse(bX), int.Parse(bY));
        Distance = DetermineDistance(X, Y, ClosestBeacon.X, ClosestBeacon.Y);
    }

    public long X { get; }

    public long Y { get; }

    public long Distance { get; }

    public Beacon ClosestBeacon { get; }

    public bool PointInRange(long targetX, long targetY)
    {
        var targetRange = DetermineDistance(X, Y, targetX, targetY);
        return Distance >= targetRange;
    }

    public static long DetermineDistance(long xOne, long yOne, long xTwo, long yTwo)
    {
        return Math.Abs(xOne - xTwo) + Math.Abs(yOne - yTwo);
    }

    public Range? CoverageInRow(long y, long minX, long maxX)
    {
        if (y > (Y + Distance) || y < (Y - Distance))
            return null;

        var rem = Math.Abs(Distance - Math.Abs(Y - y));
        var min = (X - rem) < minX ? minX : X - rem;
        var max = (X + rem) > maxX ? maxX : X + rem;
        return new Range(min, max);
    }
}