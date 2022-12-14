namespace AdventOfCode2022.CSharp.Fourteen;

public class DayFourteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Distress Signal";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 14;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Fourteen\DayFourteenInput.txt";
        var count = CalculateAmountSandBeforeDone(filePath, false);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Fourteen\DayFourteenInput.txt";
        var count = CalculateAmountSandBeforeDone(filePath, true);

        return count.ToString();
    }

    public int CalculateAmountSandBeforeDone(string filePath, bool hasFloor)
    {
        var geology = new Geology(filePath);
        var sandCount = 0;

        do
        {
            sandCount++;
            var done = hasFloor ? geology.AddGrainSandWithFloor(500, 0) : geology.AddGrainSand(500, 0);

            if (done)
                return hasFloor ? sandCount : sandCount - 1;
        } while (true);
    }
}