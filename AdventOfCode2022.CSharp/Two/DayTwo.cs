using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Two;

public class DayTwo : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Rock Paper Scissors";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 2;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Two\DayTwoInput.txt";
        var score = ScoreGame(filePath, false);

        return score.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Two\DayTwoInput.txt";
        var score = ScoreGame(filePath, true);

        return score.ToString();
    }

    public int ScoreGame(string filePath, bool determineShape)
    {
        List<int> rounds = FileUtility.ParseFileToList(filePath, line => ScoreRound(line, determineShape));
        return rounds.Sum();
    }

    private int ScoreRound(string round, bool determineShape)
    {
        var them = round[0];

        // If determineShape = true, we have to figure out the hand position rather than being assigned
        var you = determineShape ? DetermineShape(them, round[2]) : round[2];

        var total = CalculateWinPoints(them, you) + CalculateSelectionPoints(you);
        return total;
    }

    // X = lose, Y = draw, Z = win
    private char DetermineShape(char them, char winCondition)
    {
        switch (them)
        {
            case 'A':
                return winCondition == 'X' ? 'Z' : winCondition == 'Y' ? 'X' : 'Y';

            case 'B':
                return winCondition == 'X' ? 'X' : winCondition == 'Y' ? 'Y' : 'Z';

            case 'C':
                return winCondition == 'X' ? 'Y' : winCondition == 'Y' ? 'Z' : 'X';

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private int CalculateWinPoints(char them, char you)
    {
        switch (them)
        {
            case 'A':
                return you == 'Y' ? 6 : you == 'X' ? 3 : 0;

            case 'B':
                return you == 'Z' ? 6 : you == 'Y' ? 3 : 0;

            case 'C':
                return you == 'X' ? 6 : you == 'Z' ? 3 : 0;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private int CalculateSelectionPoints(char you)
    {
        return you == 'X' ? 1 : you == 'Y' ? 2 : 3;
    }
}