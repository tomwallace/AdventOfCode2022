using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Twenty;

public class DayTwenty : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Grove Positioning System [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 20;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Twenty\DayTwentyInput.txt";
        var sum = SumGroveCoordinatesUsingLinkedList(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Twenty\DayTwentyInput.txt";
        var sum = SumGroveCoordinatesUsingLinkedList(filePath, 811589153, 10);

        return sum.ToString();
    }

    // I really struggled with this one, in that my index calculations should have worked.  Suggestion on Reddit to use
    // a LinkedList, which worked for example problem.  Issue ended up being that real input had duplicates, where the
    // sample problem did not.  Then I had to rework the solution to eliminate duplicates by holding onto the original index.
    public long SumGroveCoordinatesUsingLinkedList(string filePath)
    {
        var original = FileUtility.ParseFileToList(filePath, line => long.Parse(line)).ToArray();
        var handleDupes = original.Select((val, i) => new MixNode(val, i)).ToArray();
        var copy = new LinkedList<MixNode>(handleDupes);

        Mix(copy);

        return CalculateGroveCoordinateSum(copy);
    }

    public long SumGroveCoordinatesUsingLinkedList(string filePath, long decryptionKey, int mixTimes)
    {
        var original = FileUtility.ParseFileToList(filePath, line => long.Parse(line)).ToArray();
        var handleDupes = original.Select((val, i) => new MixNode((val * decryptionKey), i)).ToArray();
        var copy = new LinkedList<MixNode>(handleDupes);

        for (int i = 0; i < mixTimes; i++)
            Mix(copy);

        return CalculateGroveCoordinateSum(copy);
    }

    private long CalculateGroveCoordinateSum(LinkedList<MixNode> copy)
    {
        var zeroItem = copy.First(c => c.Shift == 0);
        var initial = copy.Find(zeroItem);
        var first = MoveAndFind(initial, 1000, copy);
        var second = MoveAndFind(initial, 2000, copy);
        var third = MoveAndFind(initial, 3000, copy);

        return first + second + third;
    }

    public void Mix(LinkedList<MixNode> input)
    {
        var n = input.Count - 1;
        for (var i = 0; i < input.Count; i++)
        {
            var current = GetNodes(input).First(node => node.Value.ShiftOrder == i);
            var shift = ((current.Value.Shift % n) + n) % n;
            if (shift == 0)
            {
                continue;
            }
            var pointer = current;
            for (int j = 0; j < shift; j++)
                pointer = pointer.Next ?? input.First ?? throw new ArgumentException("Empty sequence!");
            var inserted = input.AddAfter(pointer, current.Value);
            input.Remove(current);
        }
    }

    public List<LinkedListNode<MixNode>> GetNodes(LinkedList<MixNode> list)
    {
        var collected = new List<LinkedListNode<MixNode>>();
        for (var i = list.First; i != null; i = i.Next)
        {
            collected.Add(i);
        }

        return collected;
    }

    public record struct MixNode(long Shift, int ShiftOrder)
    {
        public override string ToString() => $"{Shift}";
    }

    private long MoveAndFind(LinkedListNode<MixNode> pointer, int num, LinkedList<MixNode> list)
    {
        var moves = Math.Abs(num);
        // Work with remainder it larger than list
        if (moves >= list.Count)
            moves %= list.Count;

        // Move forward
        if (num > 0)
        {
            for (int i = 0; i < moves; i++)
            {
                if (pointer.Next == null)
                    pointer = list.First;
                else
                    pointer = pointer.Next;
            }
        }
        else
        {
            // Move backward
            for (int i = 0; i < moves; i++)
            {
                if (pointer.Previous == null)
                    pointer = list.Last;
                else
                    pointer = pointer.Previous;
            }
        }

        return pointer.Value.Shift;
    }
}