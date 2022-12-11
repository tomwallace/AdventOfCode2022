using AdventOfCode2022.CSharp.Utility;
using System.Text;

namespace AdventOfCode2022.CSharp.Ten;

public class DayTen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Cathode-Ray Tube";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 10;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Ten\DayTenInput.txt";
        var sum = ProcessInstructions(filePath, false);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Ten\DayTenInput.txt";
        ProcessInstructions(filePath, true);

        return "PZGPKPEB";
    }

    public int ProcessInstructions(string filePath, bool showOutput)
    {
        var instructions = FileUtility.ParseFileToList(filePath);
        var regX = 1;
        var cycle = 1;
        var recorded = new List<int>();
        var output = new List<string>();
        var builder = new StringBuilder();

        foreach (var instruction in instructions)
        {
            if (instruction == "noop")
            {
                CollectOutput(cycle, regX, builder, output);
                cycle++;
                CollectSignalStrength(cycle, regX, recorded);
            }
            else if (instruction.Contains("addx"))
            {
                CollectOutput(cycle, regX, builder, output);
                cycle++;
                CollectSignalStrength(cycle, regX, recorded);

                CollectOutput(cycle, regX, builder, output);
                regX += int.Parse(instruction.Split(' ')[1]);
                cycle++;
                CollectSignalStrength(cycle, regX, recorded);
            }
            else
                throw new ArgumentException($"Instruction ${instruction} not recognized");
        }

        // Draw the output
        if (showOutput)
        {
            foreach (var line in output)
            {
                System.Console.WriteLine(line);
            }
        }

        return recorded.Sum();
    }

    // Mutates the output and builder
    private void CollectOutput(int cycle, int regX, StringBuilder builder, List<string> output)
    {
        // Account for zero index screen layout
        var pointer = (cycle - 1) % 40;
        var regXRange = Enumerable.Range(regX - 1, 3);
        builder.Append(regXRange.Contains(pointer) ? '#' : '.');

        if (pointer == 39)
        {
            output.Add(builder.ToString());
            builder.Clear();
        }
    }

    // Mutates the recorded collection
    private void CollectSignalStrength(int cycle, int regX, List<int> recorded)
    {
        var collectAt = new List<int>() { 20, 60, 100, 140, 180, 220 };
        if (collectAt.Contains(cycle))
            recorded.Add(cycle * regX);
    }
}