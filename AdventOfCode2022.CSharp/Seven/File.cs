namespace AdventOfCode2022.CSharp.Seven;

public class File
{
    public File(string input)
    {
        // Example: 62596 h.lst
        var splits = input.Split(' ');
        Name = splits[1];
        Size = long.Parse(splits[0]);
    }

    public string Name { get; set; }

    public long Size { get; set; }
}