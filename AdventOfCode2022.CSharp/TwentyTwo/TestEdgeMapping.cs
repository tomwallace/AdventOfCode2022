namespace AdventOfCode2022.CSharp.TwentyTwo;

public class TestEdgeMapping : IEdgeMapping
{
    private Dictionary<(int, int, int), (int, int, int)> transforms;

    public TestEdgeMapping()
    {
        transforms = CreateTestTransforms();
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

    private Dictionary<(int, int, int), (int, int, int)> CreateTestTransforms()
    {
        var transforms = new Dictionary<(int, int, int), (int, int, int)>();
        // T
        // T-up (3) = LL facing down (1)
        var c = 4;
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 1, 3), (c, 5, 1));
            c--;
        }
        // T-right (0) = BR facing left (2)
        c = 12;
        for (int y = 1; y <= 4; y++)
        {
            transforms.Add((12, y, 0), (16, c, 2));
            c--;
        }
        // T-bottom (1) = F facing down (1)
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 4, 1), (x, 5, 1));
        }
        // T-left (2) = L facing down (1)
        c = 5;
        for (int y = 1; y <= 4; y++)
        {
            transforms.Add((9, y, 2), (c, 5, 1));
            c++;
        }
        // LL
        // LL-up (3) = T facing down (1)
        c = 12;
        for (int x = 1; x <= 4; x++)
        {
            transforms.Add((x, 5, 3), (c, 1, 1));
            c--;
        }
        // LL-right (0) = L facing right (0)
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((4, y, 0), (5, y, 0));
        }
        // LL-down (1) = B facing up (3)
        c = 12;
        for (int x = 1; x <= 4; x++)
        {
            transforms.Add((x, 8, 1), (c, 12, 3));
            c--;
        }
        // LL-left (2) = F facing left (2)
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((1, y, 2), (12, y, 2));
        }
        // L
        // L-up (3) = T facing right (0)
        c = 1;
        for (int x = 5; x <= 8; x++)
        {
            transforms.Add((x, 5, 3), (9, c, 0));
            c++;
        }
        // L-right (0) = F facing right (0)
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((8, y, 0), (9, y, 0));
        }
        // L-down (1) = B facing right (0)
        c = 12;
        for (int x = 5; x <= 8; x++)
        {
            transforms.Add((x, 8, 1), (9, c, 0));
            c--;
        }
        // L-left (2) = LL facing left (2)
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((5, y, 2), (4, y, 2));
        }
        // F
        // F-up (3) = T facing up (3)
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 5, 3), (x, 4, 3));
        }
        // F-right (0) = BR facing down (1)
        c = 16;
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((12, y, 0), (c, 9, 1));
            c--;
        }
        // F-down (1) = B facing down (1)
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 8, 1), (x, 9, 1));
        }
        // F-left (2) = L facing left (2)
        for (int y = 5; y <= 8; y++)
        {
            transforms.Add((9, y, 2), (8, y, 2));
        }
        // B
        // B-up (3) = F facing up (3)
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 9, 3), (x, 8, 3));
        }
        // B-right (0) = BR facing right (0)
        for (int y = 9; y <= 12; y++)
        {
            transforms.Add((12, y, 0), (13, y, 0));
        }
        // B-down (1) = LL facing up (3)
        c = 4;
        for (int x = 9; x <= 12; x++)
        {
            transforms.Add((x, 12, 1), (c, 8, 3));
            c--;
        }
        // B-left (2) = L facing up (3)
        c = 8;
        for (int y = 9; y <= 12; y++)
        {
            transforms.Add((9, y, 2), (c, 8, 3));
            c--;
        }
        // BR
        // BR-up (3) = F facing left (2)
        c = 8;
        for (int x = 13; x <= 16; x++)
        {
            transforms.Add((x, 9, 3), (8, c, 2));
            c--;
        }
        // BR-right (0) = T facing left (2)
        c = 4;
        for (int y = 9; y <= 12; y++)
        {
            transforms.Add((16, y, 0), (9, c, 2));
            c--;
        }
        // BR-down (1) = LL facing right (0)
        c = 8;
        for (int x = 13; x <= 16; x++)
        {
            transforms.Add((x, 12, 1), (1, c, 0));
            c--;
        }
        // BR-left (2) = B facing left (2)
        for (int y = 9; y <= 12; y++)
        {
            transforms.Add((13, y, 2), (12, y, 2));
        }

        return transforms;
    }
}