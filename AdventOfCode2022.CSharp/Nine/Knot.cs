namespace AdventOfCode2022.CSharp.Nine;

public class Knot
{
    public Knot(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public bool IsAdjacentOrAtop(Knot other)
    {
        if (other.X == X && other.Y == Y)
            return true;

        var acceptableX = Enumerable.Range(X - 1, 3).ToArray();
        var acceptableY = Enumerable.Range(Y - 1, 3);
        if (acceptableX.Contains(other.X) && acceptableY.Contains(other.Y))
            return true;

        return false;
    }

    public void Move(char cmd)
    {
        switch (cmd)
        {
            case 'U':
                Y++;
                break;

            case 'L':
                X++;
                break;

            case 'D':
                Y--;
                break;

            case 'R':
                X--;
                break;

            default:
                throw new ArgumentException($"{cmd} not recognized move command");
        }
    }

    public void Follow(Knot other)
    {
        if (IsAdjacentOrAtop(other))
            return;

        // Determine if can use a cardinal direction
        if (other.X == X)
        {
            Y += other.Y < Y ? -1 : 1;
            return;
        }

        if (other.Y == Y)
        {
            X += other.X < X ? -1 : 1;
            return;
        }

        // Otherwise, move one diagonally towards other
        var xMod = other.X < X ? -1 : 1;
        var yMod = other.Y < Y ? -1 : 1;
        X += xMod;
        Y += yMod;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}