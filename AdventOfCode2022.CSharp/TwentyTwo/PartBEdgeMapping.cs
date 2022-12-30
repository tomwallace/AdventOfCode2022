namespace AdventOfCode2022.CSharp.TwentyTwo;

public class PartBEdgeMapping : IEdgeMapping
{
    private Dictionary<(int, int, int), (int, int, int)> transforms;

    public PartBEdgeMapping()
    {
        transforms = CreateTransforms();
    }

    // Returns updated x, y, dir if in its dictionary
    // Otherwise returns passed x, y, dir
    public (int, int, int) Map(int x, int y, int dir)
    {
        if (transforms.ContainsKey((x, y, dir)))
            return transforms[(x, y, dir)];

        // Otherwise, move as normal
        switch (dir)
        {
            case 3:
                return (x, y - 1, dir);

            case 0:
                return (x + 1, y, dir);

            case 1:
                return (x, y + 1, dir);

            case 2:
                return (x - 1, y, dir);

            default:
                throw new ArgumentException($"Do not recongnize Dir {dir}");
        }
    }

    private Dictionary<(int, int, int), (int, int, int)> CreateTransforms()
    {
        var transforms = new Dictionary<(int, int, int), (int, int, int)>();
        // M
        // M-up (3) = T facing up (3)
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 51, 3), (x, 50, 3));
        }
        // M-right (0) = TR facing up (3)
        var c = 101;
        for (int y = 51; y <= 100; y++)
        {
            transforms.Add((100, y, 0), (c, 50, 3));
            c++;
        }
        // M-down (1) = L facing down (1)
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 100, 1), (x, 101, 1));
        }
        // M-left (2) = LL facing down (1)
        c = 1;
        for (int y = 51; y <= 100; y++)
        {
            transforms.Add((51, y, 2), (c, 101, 1));
            c++;
        }
        // T-up (3) = SL facing right (0)
        c = 151;
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 1, 3), (1, c, 0));
            c++;
        }
        // T-right (0) = TR facing right (0)
        for (int y = 1; y <= 50; y++)
        {
            transforms.Add((50, y, 0), (51, y, 0));
        }
        // T-down (1) = M facing down (1)
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 50, 1), (x, 51, 1));
        }
        // T-left (2) = LL facing right (0)
        c = 150;
        for (int y = 1; y <= 50; y++)
        {
            transforms.Add((51, y, 2), (1, c, 0));
            c--;
        }
        // TR
        // TR-up (3) = SL facing up (3)
        c = 1;
        for (int x = 101; x <= 150; x++)
        {
            transforms.Add((x, 1, 3), (c, 200, 3));
            c++;
        }
        // TR-right (0) = L facing left (2)
        c = 101;
        for (int y = 1; y <= 50; y++)
        {
            transforms.Add((150, y, 0), (100, y, 2));
            c++;
        }
        // TR-down (1) = M facing left (2)
        c = 51;
        for (int x = 101; x <= 150; x++)
        {
            transforms.Add((x, 50, 1), (100, c, 2));
            c++;
        }
        // TR-left (2) = T facing left (2)
        for (int y = 1; y <= 50; y++)
        {
            transforms.Add((101, y, 2), (100, y, 2));
        }
        // L
        // L-up (3) = M facing up (3)
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 101, 3), (x, 100, 3));
        }
        // L-right (0) = TR facing left (2)
        c = 50;
        for (int y = 101; y <= 150; y++)
        {
            transforms.Add((100, y, 0), (150, c, 2));
            c--;
        }
        // L-down (1) = SL facing left (2)
        c = 151;
        for (int x = 51; x <= 100; x++)
        {
            transforms.Add((x, 150, 1), (50, c, 2));
            c++;
        }
        // L-left (2) = LL facing left (2)
        for (int y = 101; y <= 150; y++)
        {
            transforms.Add((51, y, 2), (50, y, 2));
        }
        // LL
        // LL-up (3) = M facing right (0)
        c = 51;
        for (int x = 1; x <= 50; x++)
        {
            transforms.Add((x, 101, 3), (51, c, 0));
            c++;
        }
        // LL-right (0) = facing right (0)
        for (int y = 101; y <= 150; y++)
        {
            transforms.Add((50, y, 0), (51, y, 0));
        }
        // LL-down (1) = facing down (1)
        for (int x = 1; x <= 50; x++)
        {
            transforms.Add((x, 150, 1), (x, 151, 1));
        }
        // LL-left (2) = T facing right (0)
        c = 50;
        for (int y = 101; y <= 150; y++)
        {
            transforms.Add((1, y, 2), (51, c, 0));
            c--;
        }
        // SL
        // SL-up (3) = LL facing up (3)
        for (int x = 1; x <= 50; x++)
        {
            transforms.Add((x, 151, 3), (x, 150, 3));
        }
        // SL-right (0) = L facing up (3)
        c = 51;
        for (int y = 151; y <= 200; y++)
        {
            transforms.Add((50, y, 0), (c, 150, 3));
            c++;
        }
        // SL-down (1) = TR facing down (1)
        c = 101;
        for (int x = 1; x <= 50; x++)
        {
            transforms.Add((x, 200, 1), (1, c, 1));
            c++;
        }
        // SL-left (2) = T facing down (1)
        c = 51;
        for (int y = 151; y <= 200; y++)
        {
            transforms.Add((1, y, 2), (c, 1, 1));
            c++;
        }

        return transforms;
    }
}