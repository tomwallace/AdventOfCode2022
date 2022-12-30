namespace AdventOfCode2022.CSharp.TwentyTwo;

public interface IEdgeMapping
{
    (int, int, int) Map(int x, int y, int dir);
}