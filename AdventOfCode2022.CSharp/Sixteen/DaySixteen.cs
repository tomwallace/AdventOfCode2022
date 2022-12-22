﻿using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Sixteen;

public class DaySixteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Proboscidea Volcanium [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 16;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Sixteen\DaySixteenInput.txt";
        var max = CalculateMaxPressureReleased(filePath, false);

        return max.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Sixteen\DaySixteenInput.txt";
        var max = CalculateMaxPressureReleased(filePath, true);

        return max.ToString();
    }

    public long CalculateMaxPressureReleased(string filePath, bool useHelp)
    {
        var valves = SetUpValves(filePath);
        // Remove valves that pump 0
        var usedValveKeyRates = valves.Values.Where(v => v.ReleasePressure > 0).Select(v => (v.Name, v.ReleasePressure))
            .ToArray();

        if (useHelp)
        {
            var pressureReleasedWithHelp = GetReleasePressureWithHelp(new[] { 26, 26 }, usedValveKeyRates,
                new[] { "AA", "AA" }, valves);
            return pressureReleasedWithHelp;
        }

        var pressureReleased = GetReleasedPressure(30, usedValveKeyRates, "AA", valves);
        return pressureReleased;
    }

    private long GetReleasedPressure(int timeToGo, (string name, int rate)[] usedValveKeyRates, string startingValveKey, Dictionary<string, Valve> valves)
    {
        long best = 0;
        var current = valves[startingValveKey];

        // Iterate over all possible valves
        foreach (var t in usedValveKeyRates)
        {
            int newTimeToGo = timeToGo - current.StepsToReach[t.name] - 1;
            if (newTimeToGo > 0)
            {
                // Get the best by recursively stepping down the paths
                long gain = newTimeToGo * t.rate + GetReleasedPressure(newTimeToGo, usedValveKeyRates.Where(c => c.name != t.name).ToArray(), t.name, valves);
                if (best < gain) best = gain;
            }
        }

        return best;
    }

    private long GetReleasePressureWithHelp(int[] timeToGo, (string name, int rate)[] usedValveKeyRates,
        string[] startingValveKey, Dictionary<string, Valve> valves)
    {
        long best = 0;
        var actorIndex = timeToGo[0] > timeToGo[1] ? 0 : 1;

        var current = valves[startingValveKey[actorIndex]];

        // Iterate over all possible valves
        foreach (var t in usedValveKeyRates)
        {
            int newTimeToGo = timeToGo[actorIndex] - current.StepsToReach[t.name] - 1;
            if (newTimeToGo > 0)
            {
                var newTimes = new[] { newTimeToGo, timeToGo[1 - actorIndex] };
                var newNames = new[] { t.name, startingValveKey[1 - actorIndex] };

                // Get the best by recursively stepping down the paths
                long gain = newTimeToGo * t.rate + GetReleasePressureWithHelp(newTimes, usedValveKeyRates.Where(c => c.name != t.name).ToArray(), newNames, valves);
                if (best < gain) best = gain;
            }
        }

        return best;
    }

    private Dictionary<string, Valve> SetUpValves(string filePath)
    {
        var valves = FileUtility.ParseFileToDictionary(filePath, line =>
        {
            var valve = new Valve(line);
            return (valve.Name, valve);
        });

        // Connect them
        foreach (var key in valves.Keys)
        {
            var current = valves[key];
            foreach (var childKey in current.ConnectedValveNames)
            {
                var child = valves[childKey];
                if (!child.Connections.Exists(c => c.Name == current.Name))
                    child.Connections.Add(current);

                if (!current.Connections.Exists(c => c.Name == child.Name))
                    current.Connections.Add(child);
            }
        }

        // Determine steps to reach using BFS
        foreach (var key in valves.Keys)
        {
            var stepsToOthers = new Dictionary<string, int>();
            foreach (var otherKey in valves.Keys.Where(ok => ok != key))
            {
                var steps = MinStepToOtherUsingBfs(valves[key], valves[otherKey]);
                if (steps != null)
                    stepsToOthers.Add(otherKey, steps.Value);
            }

            valves[key].StepsToReach = stepsToOthers;
        }

        return valves;
    }

    private int? MinStepToOtherUsingBfs(Valve start, Valve target)
    {
        var previous = new Dictionary<string, Valve>();

        var queue = new Queue<(Valve, int)>();
        queue.Enqueue((start, 0));

        while (queue.Any())
        {
            var current = queue.Dequeue();
            if (current.Item1.Name == target.Name)
                return current.Item2;

            foreach (var connection in current.Item1.Connections)
            {
                // Don't revisit
                if (previous.ContainsKey(connection.Name))
                    continue;

                previous[connection.Name] = connection;
                queue.Enqueue((connection, current.Item2 + 1));
            }
        }

        // Never got there
        return null;
    }
}