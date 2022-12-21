using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Nineteen;

public class DayNineteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Not Enough Minerals [HARD]";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 19;
    }

    // TODO: Could not get either Part A or Part B
    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Nineteen\DayNineteenInput.txt";
        var sum = SumQualityLevel(filePath);

        return sum.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Nineteen\DayNineteenInput.txt";
        //var count = CountOpenSides(filePath);

        return "";
    }

    public int SumQualityLevel(string filePath)
    {
        var maxTime = 24;
        var listQualityLevels = new List<int>();
        
        var blueprints = FileUtility.ParseFileToList(filePath, line => new Blueprint(line));

        foreach (var blueprint in blueprints)
        {
            var startState = new RobotState(1,
                new Dictionary<string, int>()
                {
                    {"Ore", 0},
                    {"Clay", 0},
                    {"Obsidian", 0},
                    {"Geode", 0},
                },
                new Dictionary<string, int>()
                {
                    {"Ore", 1},
                    {"Clay", 0},
                    {"Obsidian", 0},
                    {"Geode", 0},
                },
                new Dictionary<string, bool>()
                {
                    { "Ore", false },
                    { "Clay", false },
                    { "Obsidian", false },
                    { "Geode", false }
                });


            var maxGeode = GetMaxGeodesRecursive(startState, blueprint, maxTime, new Dictionary<string, int>());

            /*
            var queue = new Queue<RobotState>();
            queue.Enqueue(startState);

            var seenBefore = new HashSet<string>() { startState.ToString() };
            var bestByTime = new Dictionary<int, int>();

            int maxGeode = 0;

            while (queue.Any())
            {
                var current = queue.Dequeue();

                // Exit condition
                if (current.Time > maxTime)
                {
                    if (current.Resources["Geode"] > maxGeode)
                        maxGeode = current.Resources["Geode"];

                    continue;
                }

                current.Time++;

                // Add options to queue
                // Test what we can make, in order of cost descending
                TryAndEnqueue("Geode", current, queue, blueprint, bestByTime);
                TryAndEnqueue("Obsidian", current, queue, blueprint, bestByTime);
                TryAndEnqueue("Clay", current, queue, blueprint, bestByTime);
                TryAndEnqueue("Ore", current, queue, blueprint, bestByTime);

                // Always add current state, as one option is not to purchase anything
                current.Collect();
                
                //if (seenBefore.Contains(current.ToString()))
                //    continue;

                if (bestByTime.ContainsKey(current.Time))
                {
                    if (bestByTime[current.Time] < current.Resources["Geode"])
                        continue;
                }
                
                bestByTime[current.Time] = current.Resources["Geode"];

                queue.Enqueue(current);
            }
            */
            listQualityLevels.Add(blueprint.Id * maxGeode);
        }

        return listQualityLevels.Sum();
    }

    private int GetMaxGeodesRecursive(RobotState current, Blueprint blueprint, int maxTime, Dictionary<string, int> cache)
    {
        if (current.Time > maxTime)
            return current.Resources["Geode"];

        if (!cache.ContainsKey(current.ToString()))
        {
            var futureStates = new List<RobotState>();
            var currentClone = current.Clone();
            currentClone.CouldBuildLastRound = current.CouldBuildLastRound;
            
            currentClone.Time++;

            // Always build a Geode bot if you can
            var geodeState = TryMakeNewState("Geode", currentClone, blueprint);
            if (geodeState != null)
            {
                geodeState.CouldBuildLastRound["Geode"] = true;
                futureStates.Add(geodeState);
            }
                

            // For others, skip building them if we already are collecting max resource needed for any of the robots
            // Because we can only build one robot per turn
            foreach (var robotName in new[] { "Obsidian", "Clay", "Ore" })
            {
                if (currentClone.Robots[robotName] >= blueprint.MaxNeeded[robotName])
                    continue;

                var newState = TryMakeNewState(robotName, currentClone, blueprint);
                if (newState != null)
                {
                    newState.CouldBuildLastRound[robotName] = true;
                    futureStates.Add(newState);
                    //break;
                }
            }

            currentClone.Collect();
            futureStates.Add(currentClone);

            cache[current.ToString()] = futureStates.Max(f => GetMaxGeodesRecursive(f, blueprint, maxTime, cache));
        }

        return cache[current.ToString()];
    }

    // Note, mutates the queue
    private void TryAndEnqueue(string robotName, RobotState current, Queue<RobotState> queue, Blueprint blueprint, Dictionary<int, int> bestByTime)
    {
        if (!blueprint.CanMake(robotName, current.Resources))
            return;

        var newState = current.Clone();
        blueprint.Make(robotName, newState.Resources);
        newState.Collect();
        newState.Robots[robotName]++;

        //if (seenBefore.Contains(newState.ToString()))
        //    return;

        if (bestByTime.ContainsKey(current.Time))
        {
            if (bestByTime[current.Time] <= current.Resources["Geode"])
                return;
        }
        bestByTime[current.Time] = current.Resources["Geode"];

        queue.Enqueue(newState);
    }

    private RobotState? TryMakeNewState(string robotName, RobotState current, Blueprint blueprint)
    {
        if (!blueprint.CanMake(robotName, current.Resources) || current.CouldBuildLastRound[robotName])
            return null;

        var newState = current.Clone();
        blueprint.Make(robotName, newState.Resources);
        newState.Collect();
        newState.Robots[robotName]++;

        return newState;
    }
}

public class RobotState
{
    public RobotState(int time, Dictionary<string, int> resources, Dictionary<string, int> robots, Dictionary<string, bool> couldBuildLastRound)
    {
        Time = time;
        Resources = resources;
        Robots = robots;
        CouldBuildLastRound = couldBuildLastRound;
    }
    
    public Dictionary<string, int> Resources { get; set; }

    public Dictionary<string, int> Robots { get; set; }

    public Dictionary<string, bool> CouldBuildLastRound {get; set; }

    public int Time { get; set; }

    public RobotState Clone()
    {
        // Assume could build none, so forces to be set
        var couldBuildPrevious = new Dictionary<string, bool>()
        {
            { "Ore", false },
            { "Clay", false },
            { "Obsidian", false },
            { "Geode", false }
        };
        return new RobotState(Time, new Dictionary<string, int>(Resources), new Dictionary<string, int>(Robots), couldBuildPrevious);
    }

    public void Collect()
    {
        foreach (var robot in Robots)
        {
            Resources[robot.Key] += robot.Value;
        }
    }

    public override string ToString()
    {
        // Used to see if we have gotten this state before, so exclude time
        var res = string.Join(",", Resources.OrderBy(i => i.Value).Select(i => i.Value));
        var rob = string.Join(",", Robots.OrderBy(i => i.Value).Select(i => i.Value));
        return $"{Time}|{res}|{rob}";
    }
}

public class Blueprint
{
    // Ex: Blueprint 1: Each ore robot costs 3 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 19 clay. Each geode robot costs 3 ore and 17 obsidian.
    public Blueprint(string input)
    {
        var splitColon = input.Split(':');
        Id = int.Parse(splitColon[0].Split(' ')[1]);

        Costs = new Dictionary<string, Dictionary<string, int>>();

        var splitPeriod = splitColon[1].Split('.', StringSplitOptions.TrimEntries);
        // Ore
        var splitOre = splitPeriod[0].Split(' ');
        Costs.Add("Ore", new Dictionary<string, int>() {{"Ore", int.Parse(splitOre[4])}});

        // Clay
        var splitClay = splitPeriod[1].Split(' ');
        Costs.Add("Clay", new Dictionary<string, int>() {{"Ore", int.Parse(splitClay[4])}});

        // Obsidian
        var splitObsidian = splitPeriod[2].Split(' ');
        Costs.Add("Obsidian", new Dictionary<string, int>()
        {
            {"Ore", int.Parse(splitObsidian[4])},
            {"Clay", int.Parse(splitObsidian[7])}
        });

        // Geode
        var splitGeode = splitPeriod[3].Split(' ');
        Costs.Add("Geode", new Dictionary<string, int>()
        {
            {"Ore", int.Parse(splitGeode[4])},
            {"Obsidian", int.Parse(splitGeode[7])}
        });

        MaxNeeded = new Dictionary<string, int>();
        
        // Need as many as we can get of Geode robots
        MaxNeeded.Add("Geode", int.MaxValue);

        foreach (var robotName in new[] { "Obsidian", "Clay", "Ore" })
        {
            var maxNeeded = Costs.Values
                .SelectMany(v => v)
                .Where(v => v.Key == robotName)
                .Select(v => v.Value)
                .Max();
            MaxNeeded.Add(robotName, maxNeeded);
        }
    }
    
    public int Id { get; }
    
    public Dictionary<string, Dictionary<string, int>> Costs { get; }

    public Dictionary<string, int> MaxNeeded { get; }

    public bool CanMake(string robotName, Dictionary<string, int> resources)
    {
        foreach (var cost in Costs[robotName])
        {
            if (resources[cost.Key] < cost.Value)
                return false;
        }

        return true;
    }

    // Note, mutates resources
    public void Make(string robotName, Dictionary<string, int> resources)
    {
        foreach (var cost in Costs[robotName])
        {
            resources[cost.Key] -= cost.Value;

            if (resources[cost.Key] < 0)
                throw new ArgumentException($"Resource {cost.Key} less than 0");
        }
    }
}

