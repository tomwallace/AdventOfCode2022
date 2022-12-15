using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Thirteen;

public class DayThirteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Distress Signal [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 13;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Thirteen\DayThirteenInput.txt";
        var sum = CountCorrectPackets(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Thirteen\DayThirteenInput.txt";
        var score = SortPackets(filePath);

        return score.ToString();
    }

    public int CountCorrectPackets(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath);
        var pairs = new List<(string, string)>();

        for (int i = 0; i < lines.Count; i += 3)
        {
            pairs.Add((lines[i], lines[i + 1]));
        }

        var sum = pairs
            .Select((p, index) => IsPacketInRightOrderParse(p.Item1, p.Item2) ? index + 1 : 0)
            .Sum();

        return sum;
    }

    public int SortPackets(string filePath)
    {
        var lines = FileUtility.ParseFileToList(filePath);
        var packets = lines.Where(l => !string.IsNullOrEmpty(l)).ToList();
        packets.Add("[[2]]");
        packets.Add("[[6]]");

        // Bubble sort
        while (!AllSorted(packets))
        {
            for (int i = 0; i < packets.Count - 1; i++)
            {
                if (!IsPacketInRightOrderParse(packets[i], packets[i + 1]))
                {
                    var temp = packets[i];
                    packets[i] = packets[i + 1];
                    packets[i + 1] = temp;
                }
            }
        }

        var two = packets.FindIndex(p => p == "[[2]]") + 1;
        var six = packets.FindIndex(p => p == "[[6]]") + 1;

        return two * six;
    }

    private bool AllSorted(List<string> packets)
    {
        for (int i = 0; i < packets.Count - 1; i++)
        {
            if (!IsPacketInRightOrderParse(packets[i], packets[i + 1]))
                return false;
        }

        return true;
    }

    public bool IsPacketInRightOrderParse(string one, string two)
    {
        var elements1 = Parse(one);
        var elements2 = Parse(two);
        return CompareLists(elements1, elements2) <= 0;
    }

    private int CompareLists(List<object> one, List<object> two)
    {
        var length = one.Count < two.Count ? one.Count : two.Count;
        for (int i = 0; i < length; i++)
        {
            var element1 = one[i];
            var element2 = two[i];
            var result = CompareElements(element1, element2);
            if (result < 0)
                return -1;

            if (result > 0)
                return 1;
        }

        // Then go with the shorter
        return Math.Sign(one.Count - two.Count);
    }

    private int CompareElements(object one, object two)
    {
        return (one, two) switch
        {
            (int f, int s) => Math.Sign(f - s),
            (int f, List<object> s) => CompareLists(new List<object>() { f }, s),
            (List<object> f, int s) => CompareLists(f, new List<object>() { s }),
            (List<object> f, List<object> s) => CompareLists(f, s),
            _ => throw new Exception($"Could not compare elements {one} vs. {two}."),
        };
    }

    private List<object> Parse(string input)
    {
        var stack = new Stack<char>();
        foreach (var c in input.ToCharArray().Reverse())
        {
            stack.Push(c);
        }

        var list = ParseList(stack);
        return list;
    }

    private List<object> ParseList(Stack<char> stack)
    {
        var elements = new List<object>();
        // Trim the starting [, assume that we have one
        stack.Pop();
        while (stack.Peek() != ']')
        {
            if (stack.Peek() == ',')
                stack.Pop();

            var element = ParseElement(stack);
            elements.Add(element);
        }

        // Trim off the final ]
        stack.Pop();

        return elements;
    }

    private object ParseElement(Stack<char> stack)
    {
        var next = stack.Peek();
        if (char.IsDigit(next))
            return ParseInt(stack);

        if (next == '[')
            return ParseList(stack);

        throw new Exception("Received unexpected character");
    }

    // This is where I went wrong, as the real input has numbers that are bigger than single-digit
    public int ParseInt(Stack<char> stack)
    {
        var token = string.Empty;
        while (char.IsDigit(stack.Peek()))
        {
            token += stack.Pop();
        }
        return int.Parse(token);
    }
}