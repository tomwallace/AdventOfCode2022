using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.TwentyOne;

public class DayTwentyOne : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Monkey Math [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 21;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"TwentyOne\DayTwentyOneInput.txt";
        var val = FindValueOfRoot(filePath);

        return val.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"TwentyOne\DayTwentyOneInput.txt";
        var equation = GetReducedEquation(filePath);

        return equation;
    }

    // I originally tried using the "splitting number range" process that has worked for other years.  You start out with 0 and long.MaxValue
    // and try running the problem with the half way point.  Then, use the output to determine if the answer is above or below and take it half
    // way in that direction.  Etc.  But, that did not work, even trying a number of times - so clearly I was doing something wrong.
    // I looked to Reddit for a suggestion and found reducing and formula as much as possible and then using an online Math calculator
    // to derive the answer.  That is what I did.
    public string GetReducedEquation(string filePath)
    {
        var monkeys = FileUtility.ParseFileToDictionary(filePath, line =>
        {
            var mp = new MonkeyPair(line);
            return (mp.Name, mp);
        });

        var leftSide = monkeys["root"].InputKeys.Item1;
        var rightSide = monkeys["root"].InputKeys.Item2;
        var leftVal = monkeys[leftSide].GetExpression(monkeys);
        var rightVal = monkeys[rightSide].GetExpression(monkeys);

        return $"{leftVal} = {rightVal}";
    }

    public long FindValueOfRoot(string filePath)
    {
        var monkeyPairs = FileUtility.ParseFileToList(filePath, line => new MonkeyPair(line));
        var solvedMonkeys = new Dictionary<string, long>();
        var temp = new List<MonkeyPair>();

        for (int i = 0; i < monkeyPairs.Count; i++)
        {
            if (monkeyPairs[i].InitialNumber != null)
            {
                solvedMonkeys.Add(monkeyPairs[i].Name, monkeyPairs[i].InitialNumber!.Value);
            }
            else
            {
                temp.Add(monkeyPairs[i]);
            }
        }

        monkeyPairs = temp.ToList();
        var processedRoot = false;

        while (monkeyPairs.Any() || processedRoot == false)
        {
            temp.Clear();

            for (int i = 0; i < monkeyPairs.Count; i++)
            {
                if (monkeyPairs[i].CanProcess(solvedMonkeys))
                {
                    solvedMonkeys.Add(monkeyPairs[i].Name, monkeyPairs[i].Solve(solvedMonkeys));

                    if (monkeyPairs[i].Name == "root")
                        processedRoot = true;
                }
                else
                {
                    temp.Add(monkeyPairs[i]);
                }
            }

            monkeyPairs = temp.ToList();
        }

        return solvedMonkeys["root"];
    }
}