namespace AdventOfCode2022.CSharp.TwentyTwo;

public class PartAEdgeMapping : IEdgeMapping
{
    private Dictionary<int, (int, int)> rowMask;
    private Dictionary<int, (int, int)> colMask;

    public PartAEdgeMapping(Dictionary<int, (int, int)> rowMask, Dictionary<int, (int, int)> colMask)
    {
        this.rowMask = rowMask;
        this.colMask = colMask;
    }

    public (int, int, int) Map(int x, int y, int dir)
    {
        switch (dir)
        {
            case 3:
                return (x, WrapY(x, y - 1), dir);

            case 0:
                return (WrapX(x + 1, y), y, dir);

            case 1:
                return (x, WrapY(x, y + 1), dir);

            case 2:
                return (WrapX(x - 1, y), y, dir);

            default:
                throw new ArgumentException($"Do not recongnize Dir {dir}");
        }
    }

    private int WrapX(int x, int y)
    {
        if (x > rowMask[y].Item2)
            return rowMask[y].Item1;
        if (x < rowMask[y].Item1)
            return rowMask[y].Item2;

        return x;
    }

    private int WrapY(int x, int y)
    {
        if (y > colMask[x].Item2)
            return colMask[x].Item1;
        if (y < colMask[x].Item1)
            return colMask[x].Item2;

        return y;
    }
}