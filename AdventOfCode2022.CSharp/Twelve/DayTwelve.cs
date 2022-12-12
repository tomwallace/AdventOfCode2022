using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Twelve;

public class DayTwelve : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Hill Climbing Algorithm [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 12;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Twelve\DayTwelveInput.txt";
        var min = FindMinStepsQueue(filePath, false);

        return min.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Twelve\DayTwelveInput.txt";
        var min = FindMinStepsQueue(filePath, true);

        return min.ToString();
    }

    public int FindMinStepsQueue(string filePath, bool multipleStarts)
    {
        var map = FileUtility.ParseFileToList(filePath, line => line.ToCharArray().ToList());

        List<HillStep> potentialStarts = new List<HillStep>();
        HillStep initialStart = null;
        HillStep end = null;
        for (int currY = 0; currY < map.Count; currY++)
        {
            for (int currX = 0; currX < map[0].Count; currX++)
            {
                if (map[currY][currX] == 'a')
                {
                    potentialStarts.Add(new HillStep(currX, currY, (int)'a'));
                }

                if (map[currY][currX] == 'S')
                {
                    initialStart = new HillStep(currX, currY, (int)'a');
                    potentialStarts.Add(initialStart);
                    map[currY][currX] = 'a';
                }

                if (map[currY][currX] == 'E')
                {
                    end = new HillStep(currX, currY, (int)'z');
                    map[currY][currX] = 'z';
                }
            }
        }

        if (initialStart == null || end == null || !potentialStarts.Any())
            throw new ArgumentException("Could not find start or end");

        var starts = multipleStarts ? potentialStarts : new List<HillStep>() { initialStart };

        var min = starts.Select(p => FindMinStepsForStartingPointQueue(p, end, map)).Min();
        return min;
    }

    public int FindMinStepsForStartingPointQueue(HillStep start, HillStep end, List<List<char>> map)
    {
        if (start == null || end == null)
            throw new ArgumentException("Could not find start or end");

        var pathLengths = new List<int>();
        var stepsToHillStep = new Dictionary<string, int>() { { start.ToString(), 1 } };

        var queue = new Queue<HillStep>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            // Exit condition
            if (current.Equals(end))
            {
                pathLengths.Add(current.PreviousSteps.Count);
                continue;
            }

            var nextSteps = current.NextSteps(map);

            foreach (var step in nextSteps)
            {
                // Check to see if we have reached that step with fewer steps already
                if (stepsToHillStep.ContainsKey(step.ToString()) &&
                    stepsToHillStep[step.ToString()] <= step.PreviousSteps.Count)
                    continue;

                stepsToHillStep.Add(step.ToString(), step.PreviousSteps.Count);
                queue.Enqueue(step);
            }
        }

        if (!pathLengths.Any())
            return int.MaxValue;

        var min = pathLengths.Min();
        return min - 1;
    }
}