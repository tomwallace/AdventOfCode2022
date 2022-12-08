using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Seven;

public class DaySeven : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "No Space Left On Device";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 7;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Seven\DaySevenInput.txt";
        var sum = CreateDirectoryStructureAndEvaluate(filePath, 100000);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Seven\DaySevenInput.txt";
        var smallest = CreateDirectoryStructureAndFindDelete(filePath);

        return smallest.ToString();
    }

    public long CreateDirectoryStructureAndEvaluate(string filePath, long maxSize)
    {
        var instructions = FileUtility.ParseFileToList(filePath);
        var root = ProcessInstructions(instructions);
        var sum = SumDirectoriesOverSize(root, maxSize);
        return sum;
    }

    public long CreateDirectoryStructureAndFindDelete(string filePath)
    {
        var updateSize = 30000000;
        var instructions = FileUtility.ParseFileToList(filePath);
        var root = ProcessInstructions(instructions);
        var target = updateSize - (70000000 - root.DirectorySize());
        var smallestSuccess = SmallestDirectoryToSucceed(root, target);
        return smallestSuccess;
    }

    public long SumDirectoriesOverSize(Directory current, long maxSize)
    {
        var currentSize = current.DirectorySize() <= maxSize ? current.DirectorySize() : 0;
        return currentSize + current.SubDirectories.Sum(s => SumDirectoriesOverSize(s, maxSize));
    }

    public long SmallestDirectoryToSucceed(Directory current, long target)
    {
        var currentSize = current.DirectorySize();
        if (currentSize <= target)
            currentSize = long.MaxValue;

        if (!current.SubDirectories.Any())
            return currentSize;

        var childrenMin = current.SubDirectories.Min(s => SmallestDirectoryToSucceed(s, target));
        return Math.Min(currentSize, childrenMin);
    }

    public Directory ProcessInstructions(List<string> instructions)
    {
        var root = new Directory("dir /");
        var current = root;
        for (int i = 0; i < instructions.Count; i++)
        {
            var instruction = instructions[i];
            if (instruction.Contains("$ cd"))
            {
                var splits = instruction.Split(' ');
                var key = splits[2];
                if (key == "/")
                {
                    current = root;
                    continue;
                }

                if (key == "..")
                {
                    current = current.Parent;
                    continue;
                }

                current = current.SubDirectories.First(s => s.Name == key);
                continue;
            }

            if (instruction == "$ ls")
            {
                while ((i + 1) < instructions.Count() && !instructions[i + 1].Contains("$"))
                {
                    i++;
                    instruction = instructions[i];
                    if (instruction.Contains("dir"))
                    {
                        var sub = new Directory(instruction);
                        sub.Parent = current;
                        current.SubDirectories.Add(sub);
                    }
                    else
                    {
                        current.Files.Add(new File(instruction));
                    }
                }
            }
        }

        return root;
    }
}