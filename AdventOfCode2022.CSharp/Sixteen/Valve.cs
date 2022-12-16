namespace AdventOfCode2022.CSharp.Sixteen;

public class Valve
{
    // Ex: Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
    // Ex: Valve HH has flow rate=22; tunnel leads to valve GG
    // Note tunnel leads and tunnels lead
    public Valve(string input)
    {
        var splits = input.Split(';');

        var firstHalfSpace = splits[0].Split(' ');
        Name = firstHalfSpace[1];

        var firstHalfEqual = splits[0].Split('=');
        ReleasePressure = int.Parse(firstHalfEqual[1]);

        if (splits[1].Contains("tunnels lead to valves"))
        {
            var secondHalfDetails =
                splits[1].Split(new string[] { " tunnels lead to valves " }, StringSplitOptions.TrimEntries);
            ConnectedValveNames = secondHalfDetails[1].Split(',', StringSplitOptions.TrimEntries).ToList();
        }
        else
        {
            var secondHalfDetails = splits[1].Split(new string[] { " tunnel leads to valve " }, StringSplitOptions.TrimEntries);
            ConnectedValveNames = new List<string>() { secondHalfDetails[1] };
        }

        Connections = new List<Valve>();
    }

    public string Name { get; }

    public int ReleasePressure { get; }

    public List<string> ConnectedValveNames { get; }

    public List<Valve> Connections { get; set; }

    public Dictionary<string, int> StepsToReach { get; set; }

    public override string ToString()
    {
        return Name;
    }
}