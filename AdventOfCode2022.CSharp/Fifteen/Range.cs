namespace AdventOfCode2022.CSharp.Fifteen;

/// <summary>
/// Represent range as a start and end value, so we don't have to care about the middle
/// </summary>
public class Range
{
    public Range(long start, long end)
    {
        Start = start;
        End = end;
    }

    public long Start { get; set; }

    public long End { get; set; }

    // Collapse list of Ranges down to remove duplication, critical to solving Part B
    // Found method - https://stackoverflow.com/questions/1233292/whats-a-good-generic-algorithm-for-collapsing-a-set-of-potentially-overlapping
    public static IEnumerable<Range> Collapse(IEnumerable<Range?> me)
    {
        // Remove null values
        var orderedList = me.Where(r => r != null).OrderBy(r => r.Start).ToList();
        var newList = new List<Range>();

        var max = orderedList[0].End;
        var min = orderedList[0].Start;

        foreach (var item in orderedList.Skip(1))
        {
            if (item.End > max && item.Start > max)
            {
                newList.Add(new Range(min, max));
                min = item.Start;
            }
            max = max > item.End ? max : item.End;
        }
        newList.Add(new Range(min, max));

        return newList;
    }
}