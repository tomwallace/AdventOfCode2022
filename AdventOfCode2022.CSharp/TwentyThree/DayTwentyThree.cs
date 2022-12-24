namespace AdventOfCode2022.CSharp.TwentyThree;

public class DayTwentyThree : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Unstable Diffusion";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 23;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyThree\DayTwentyThreeInput.txt";
        var count = CountEmptySpace(filePath, 10);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyThree\DayTwentyThreeInput.txt";
        var rounds = RoundsUntilNoElfMoved(filePath);

        return rounds.ToString();
    }

    public int RoundsUntilNoElfMoved(string filePath)
    {
        var moved = true;
        var round = 0;

        var crater = new Crater(filePath);

        do
        {
            round++;
            moved = crater.CompleteRound();
        } while (moved);

        return round;
    }

    public int CountEmptySpace(string filePath, int rounds)
    {
        var crater = new Crater(filePath);
        //Debug.WriteLine("Round 0");
        //crater.Print();
        for (int i = 0; i < rounds; i++)
        {
            crater.CompleteRound();
            //Debug.WriteLine($"Round {i + 1}");
            //crater.Print();
        }

        return crater.CountEmptyInRange();
    }
}