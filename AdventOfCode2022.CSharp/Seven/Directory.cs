namespace AdventOfCode2022.CSharp.Seven;

public class Directory
{
    public Directory(string input)
    {
        // Example: dir e
        var splits = input.Split(' ');
        Name = splits[1];

        Files = new List<File>();
        SubDirectories = new List<Directory>();
    }

    public string Name { get; set; }

    public List<File> Files { get; set; }

    public List<Directory> SubDirectories { get; set; }

    public Directory? Parent { get; set; }

    public long DirectorySize()
    {
        var total = Files.Sum(f => f.Size) + SubDirectories.Sum(s => s.DirectorySize());
        return total;
    }
}