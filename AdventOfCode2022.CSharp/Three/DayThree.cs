using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Three;

public class DayThree : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Rucksack Reorganization";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 3;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Three\DayThreeInput.txt";
        var rucksacks = FileUtility.ParseFileToList(filePath, line => line);
        var sum = GetPriorityScoreSum(rucksacks, false);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Three\DayThreeInput.txt";
        var rucksacks = FileUtility.ParseFileToList(filePath, line => line);
        var sum = GetPriorityScoreSum(rucksacks, true);

        return sum.ToString();
    }

    public int GetPriorityScoreSum(List<string> rucksacks, bool useGroups)
    {
        var items = useGroups ? GetGroupCommonItems(rucksacks) : GetDuplicateItems(rucksacks);
        var scores = items.Select(CalculatePriorityScore);
        return scores.Sum();
    }

    public List<char> GetDuplicateItems(List<string> rucksacks)
    {
        var duplicates = new List<char>();
        foreach (var sack in rucksacks)
        {
            var split = sack.Length / 2;
            var firstHalf = sack.Substring(0, split);
            foreach (var c in sack.Substring(split))
            {
                if (firstHalf.Contains(c))
                {
                    duplicates.Add(c);
                    break;
                }
            }
        }

        return duplicates;
    }

    public List<char> GetGroupCommonItems(List<string> rucksacks)
    {
        var commonItems = new List<char>();
        for (int i = 0; i < rucksacks.Count; i += 3)
        {
            var group = rucksacks.Skip(i).Take(3).ToList();
            foreach (var c in group[0].ToCharArray())
            {
                if (group[1].Contains(c) && group[2].Contains(c))
                {
                    commonItems.Add(c);
                    break;
                }
            }
        }

        // Sanity check
        if (commonItems.Count != rucksacks.Count / 3)
            throw new Exception("Did not find enough common items");

        return commonItems;
    }

    private int CalculatePriorityScore(char item)
    {
        if (char.IsLower(item))
            return (int)item - 96;

        return (int)item - 38;
    }
}