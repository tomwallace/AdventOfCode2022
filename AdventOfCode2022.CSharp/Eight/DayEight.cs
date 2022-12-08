using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Eight;

public class DayEight : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Treetop Tree House";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 8;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Eight\DayEightInput.txt";
        var count = CountVisibleTrees(filePath);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Eight\DayEightInput.txt";
        var score = FindBestScenicScore(filePath);

        return score.ToString();
    }

    public int CountVisibleTrees(string filePath)
    {
        var grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToList());
        var count = 0;
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                if (IsTreeVisible(x, y, grid))
                    count++;
            }
        }

        return count;
    }

    public int FindBestScenicScore(string filePath)
    {
        var grid = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToList());
        var score = 0;
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Count; x++)
            {
                var currentScore = CalculateScenicScore(x, y, grid);
                if (currentScore > score)
                    score = currentScore;
            }
        }

        return score;
    }

    private bool IsTreeVisible(int currX, int currY, List<List<int>> grid)
    {
        // Edges automatically succeed
        if (currX == 0 || currX == grid[0].Count - 1 || currY == 0 || currY == grid.Count - 1)
            return true;

        var currValue = grid[currY][currX];

        // check to West
        for (int x = currX - 1; x >= 0; x--)
        {
            if (currValue <= grid[currY][x])
                break;

            if (x == 0)
                return true;
        }

        // check to North
        for (int y = currY - 1; y >= 0; y--)
        {
            if (currValue <= grid[y][currX])
                break;

            if (y == 0)
                return true;
        }

        // check to East
        for (int x = currX + 1; x < grid[0].Count; x++)
        {
            if (currValue <= grid[currY][x])
                break;

            if (x == grid[0].Count - 1)
                return true;
        }

        // check to South
        for (int y = currY + 1; y < grid.Count; y++)
        {
            if (currValue <= grid[y][currX])
                break;

            if (y == grid.Count - 1)
                return true;
        }

        // Otherwise, it is not visible
        return false;
    }

    private int CalculateScenicScore(int currX, int currY, List<List<int>> grid)
    {
        var currValue = grid[currY][currX];

        // check to West
        var westScore = 0;
        for (int x = currX - 1; x >= 0; x--)
        {
            westScore++;

            if (currValue <= grid[currY][x])
                break;
        }

        // check to North
        var northScore = 0;
        for (int y = currY - 1; y >= 0; y--)
        {
            northScore++;

            if (currValue <= grid[y][currX])
                break;
        }

        // check to East
        var eastScore = 0;
        for (int x = currX + 1; x < grid[0].Count; x++)
        {
            eastScore++;

            if (currValue <= grid[currY][x])
                break;
        }

        // check to South
        var southScore = 0;
        for (int y = currY + 1; y < grid.Count; y++)
        {
            southScore++;

            if (currValue <= grid[y][currX])
                break;
        }

        return westScore * northScore * eastScore * southScore;
    }
}