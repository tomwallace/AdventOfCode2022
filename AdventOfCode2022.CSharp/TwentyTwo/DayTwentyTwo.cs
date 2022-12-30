namespace AdventOfCode2022.CSharp.TwentyTwo;

public class DayTwentyTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Monkey Map [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 22;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var password = GetPassword(filePath);

        return password.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyTwo\DayTwentyTwoInput.txt";
        var password = GetPasswordForCube(filePath, new PartBEdgeMapping());

        return password.ToString();
    }

    public int GetPassword(string filePath)
    {
        var map = new Map(filePath);
        foreach (var ins in map.Instructions)
        {
            map.FollowInstruction(ins, new PartAEdgeMapping(map.RowMask, map.ColMask));
            //map.Print();
        }

        return map.Current.GetPassword();
    }

    public int GetPasswordForCube(string filePath, IEdgeMapping mapping)
    {
        var map = new Map(filePath);
        foreach (var ins in map.Instructions)
        {
            map.FollowInstruction(ins, mapping);
            //map.Print();
        }

        return map.Current.GetPassword();
    }
}