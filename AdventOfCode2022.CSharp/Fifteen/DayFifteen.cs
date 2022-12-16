using AdventOfCode2022.CSharp.Utility;

namespace AdventOfCode2022.CSharp.Fifteen;

public class DayFifteen : IAdventProblemSet
{
    /// <inheritdoc />
    public string Description()
    {
        return "Beacon Exclusion Zone";
    }

    /// <inheritdoc />
    public int SortOrder()
    {
        return 15;
    }

    /// <inheritdoc />
    public string PartA()
    {
        string filePath = @"Fifteen\DayFifteenInput.txt";
        var count = CountRowLocationsNoBeacon(filePath, 2000000);

        return count.ToString();
    }

    /// <inheritdoc />
    public string PartB()
    {
        string filePath = @"Fifteen\DayFifteenInput.txt";
        var frequency = FindTuningFrequencyOfBeacon(filePath, 0, 4000000);

        return frequency.ToString();
    }

    public int CountRowLocationsNoBeacon(string filePath, long rowIndex)
    {
        var sensors = FileUtility.ParseFileToList(filePath, line => new Sensor(line));
        var lowestX = sensors.Select(s => s.X - s.Distance).Min();
        var highestX = sensors.Select(s => s.X + s.Distance).Max();

        var xRange = new Range(lowestX, highestX);
        var noBeacon = 0;
        for (var x = xRange.Start; x <= xRange.End; x++)
        {
            if (sensors.Any(s => s.PointInRange(x, rowIndex)))
                noBeacon++;
        }

        return noBeacon - 1;
    }

    public long FindTuningFrequencyOfBeacon(string filePath, long min, long max)
    {
        var sensors = FileUtility.ParseFileToList(filePath, line => new Sensor(line));

        // Go row by row until we find one that has a possibility
        for (long y = 0; y <= max; y++)
        {
            var coverage = sensors.Select(s => s.CoverageInRow(y, min, max));
            var collapsedCoverage = Range.Collapse(coverage);

            if (collapsedCoverage.Count() > 1)
            {
                // Now find x
                var x = collapsedCoverage.Select(c => c.End).Min() + 1;
                return (long)(x * 4000000) + y;
            }
        }

        throw new Exception("Should never reach here");
    }
}