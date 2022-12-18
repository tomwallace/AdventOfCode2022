namespace AdventOfCode2022.CSharp.Seventeen;

public class Shape
{
    public Shape(long remainder, long xMin, long xMax, long yCeiling)
    {
        switch (remainder)
        {
            // Line
            case 0:
                positions = new List<Point>()
                {
                    new Point(xMin + 3, yCeiling + 4),
                    new Point(xMin + 4, yCeiling + 4),
                    new Point(xMin + 5, yCeiling + 4),
                    new Point(xMin + 6, yCeiling + 4)
                };
                break;
            // Plus
            case 1:
                positions = new List<Point>()
                {
                    new Point(xMin + 3, yCeiling + 5),
                    new Point(xMin + 4, yCeiling + 5),
                    new Point(xMin + 5, yCeiling + 5),
                    new Point(xMin + 4, yCeiling + 4),
                    new Point(xMin + 4, yCeiling + 6)
                };
                break;
            // Backwards L
            case 2:
                positions = new List<Point>()
                {
                    new Point(xMin + 3, yCeiling + 4),
                    new Point(xMin + 4, yCeiling + 4),
                    new Point(xMin + 5, yCeiling + 4),
                    new Point(xMin + 5, yCeiling + 5),
                    new Point(xMin + 5, yCeiling + 6)
                };
                break;
            // |
            case 3:
                positions = new List<Point>()
                {
                    new Point(xMin + 3, yCeiling + 4),
                    new Point(xMin + 3, yCeiling + 5),
                    new Point(xMin + 3, yCeiling + 6),
                    new Point(xMin + 3, yCeiling + 7)
                };
                break;
            // Square
            case 4:
                positions = new List<Point>()
                {
                    new Point(xMin + 3, yCeiling + 4),
                    new Point(xMin + 4, yCeiling + 4),
                    new Point(xMin + 3, yCeiling + 5),
                    new Point(xMin + 4, yCeiling + 5)
                };
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private List<Point> positions;

    public List<Point> GetCurrentPositions => positions;

    public List<Point> TryMoveLaterally(char inst)
    {
        if (inst != '<' && inst != '>')
            throw new ArgumentException($"Inst of {inst} not recognized");

        var modifier = inst == '<' ? -1 : 1;
        return positions.Select(p => new Point(p.X + modifier, p.Y)).ToList();
    }

    public List<Point> TryMoveDown()
    {
        return positions.Select(p => new Point(p.X, p.Y - 1)).ToList();
    }

    public void SetPositions(List<Point> newPositions)
    {
        positions = newPositions;
    }
}