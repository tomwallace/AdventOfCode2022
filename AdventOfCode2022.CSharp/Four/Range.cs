namespace AdventOfCode2022.CSharp.Four;

public class Range
{
    public Range(string line)
    {
        var split = line.Split('-');
        Min = int.Parse(split[0]);
        Max = int.Parse(split[1]);
        Actual = Enumerable.Range(Min, Max - Min + 1).ToArray();
    }

    public int Min { get; set; }

    public int Max { get; set; }

    public int[] Actual { get; set; }

    public override string ToString()
    {
        return $"{Min}-{Max}";
    }
}