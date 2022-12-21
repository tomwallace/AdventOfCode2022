namespace AdventOfCode2022.CSharp.TwentyOne;

public class MonkeyPair
{
    /*
     * Ex: szfd: 6
           vzfl: ftpn * vcrl
     */

    public MonkeyPair(string input)
    {
        var splitColon = input.Split(':', StringSplitOptions.TrimEntries);
        Name = splitColon[0];
        var splitSpace = splitColon[1].Split(' ');
        if (splitSpace.Length == 1)
        {
            InitialNumber = long.Parse(splitSpace[0]);
        }
        else
        {
            InputKeys = (splitSpace[0], splitSpace[2]);
            switch (splitSpace[1])
            {
                case "+":
                    Operation = (one, two) => one + two;
                    OperationString = "+";
                    break;

                case "-":
                    Operation = (one, two) => one - two;
                    OperationString = "-";
                    break;

                case "*":
                    Operation = (one, two) => one * two;
                    OperationString = "*";
                    break;

                case "/":
                    Operation = (one, two) => one / two;
                    OperationString = "/";
                    break;

                default:
                    throw new ArgumentException($"Not recognized operation: {splitSpace[1]}");
            }
        }
    }

    public string Name { get; }

    public (string, string) InputKeys { get; }

    public Func<long, long, long>? Operation { get; }

    public long? InitialNumber { get; }

    public string? OperationString { get; }

    public bool CanProcess(Dictionary<string, long> solvedMonkeys)
    {
        return solvedMonkeys.ContainsKey(InputKeys.Item1) && solvedMonkeys.ContainsKey(InputKeys.Item2);
    }

    public long Solve(Dictionary<string, long> solvedMonkeys)
    {
        return Operation!(solvedMonkeys[InputKeys.Item1], solvedMonkeys[InputKeys.Item2]);
    }

    // Recursively generate the actual math expression
    public string GetExpression(Dictionary<string, MonkeyPair> monkeys)
    {
        if (InitialNumber != null)
            return InitialNumber.Value.ToString();

        var leftSide = InputKeys.Item1 == "humn" ? "x" : monkeys[InputKeys.Item1].GetExpression(monkeys);
        var rightSide = InputKeys.Item2 == "humn" ? "x" : monkeys[InputKeys.Item2].GetExpression(monkeys);

        if (leftSide.Contains("x") || rightSide.Contains("x"))
            return "(" + leftSide + OperationString + rightSide + ")";

        if (long.TryParse(leftSide, out var leftVal) && long.TryParse(rightSide, out var rightVal))
        {
            //evaluate to save space

            if (Operation != null)
            {
                var result = Operation(leftVal, rightVal);

                return result.ToString();
            }
        }

        return "(" + leftSide + OperationString + rightSide + ")";
    }
}