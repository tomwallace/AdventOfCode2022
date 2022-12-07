using AdventOfCode2022.CSharp.Utility;
using System.Text;

namespace AdventOfCode2022.CSharp.Five;

public class DayFive : IAdventProblemSet
{
    /*
    [S]                 [T] [Q]
    [L]             [B] [M] [P]     [T]
    [F]     [S]     [Z] [N] [S]     [R]
    [Z] [R] [N]     [R] [D] [F]     [V]
    [D] [Z] [H] [J] [W] [G] [W]     [G]
    [B] [M] [C] [F] [H] [Z] [N] [R] [L]
    [R] [B] [L] [C] [G] [J] [L] [Z] [C]
    [H] [T] [Z] [S] [P] [V] [G] [M] [M]
    1   2   3   4   5   6   7   8   9
     */

    private Dictionary<int, List<char>> stackInput = new Dictionary<int, List<char>>()
    {
        { 1, new List<char>() { 'H', 'R', 'B', 'D', 'Z', 'F', 'L', 'S' } },
        { 2, new List<char>() { 'T', 'B', 'M', 'Z', 'R' } },
        { 3, new List<char>() { 'Z', 'L', 'C', 'H', 'N', 'S' } },
        { 4, new List<char>() { 'S', 'C', 'F', 'J' } },
        { 5, new List<char>() { 'P', 'G', 'H', 'W', 'R', 'Z', 'B' } },
        { 6, new List<char>() {'V','J','Z','G','D','N','M','T'}},
        { 7, new List<char>() {'G','L','N','W','F','S','P','Q'}},
        { 8, new List<char>() {'M','Z','R'}},
        { 9, new List<char>() {'M','C','L','G','V','R','T'}}
    };

    /// <inheritdoc />
    public string Description()
    {
        return "Supply Stacks";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 5;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Five\DayFiveInput.txt";
        var directions = FileUtility.ParseFileToList(filePath);
        var result = MoveAndGetTopCrates(directions, stackInput, true);

        return result;
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Five\DayFiveInput.txt";
        var directions = FileUtility.ParseFileToList(filePath);
        var result = MoveAndGetTopCrates(directions, stackInput, false);

        return result;
    }

    public string MoveAndGetTopCrates(List<string> directions, Dictionary<int, List<char>> stacks, bool reorder)
    {
        foreach (var direction in directions)
        {
            var splits = direction.Split(' ');
            var number = int.Parse(splits[1]);
            var from = int.Parse(splits[3]);
            var to = int.Parse(splits[5]);

            var crates = Remove(stacks, number, from, reorder);
            Add(stacks, to, crates);
        }

        var builder = new StringBuilder();
        foreach (var val in stacks.Values)
        {
            builder.Append(val[val.Count - 1]);
        }
        return builder.ToString();
    }

    // Pulls off number crates from the end of the "from" stack and then reverses the order of them
    // NOTE: Mutates stacks
    private List<char> Remove(Dictionary<int, List<char>> stacks, int number, int from, bool reorder)
    {
        var crates = stacks[from].GetRange(stacks[from].Count - number, number);

        if (reorder)
            crates.Reverse();

        stacks[from].RemoveRange(stacks[from].Count - number, number);
        return crates;
    }

    private void Add(Dictionary<int, List<char>> stacks, int to, List<char> crates)
    {
        stacks[to].AddRange(crates);
    }
}