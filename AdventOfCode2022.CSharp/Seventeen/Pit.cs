using System.Diagnostics;
using System.Text;

namespace AdventOfCode2022.CSharp.Seventeen;

public class Pit
{
    private int pointer;

    public Pit(long xMin, long xMax, long yCeiling, List<char> instructions)
    {
        XMin = xMin;
        XMax = xMax;
        YCeiling = yCeiling;

        Occupied = new Dictionary<string, long>();
        NumberOfShapes = 0;
        pointer = 0;

        Instructions = instructions;
    }

    public List<char> Instructions { get; set; }

    public long NumberOfShapes { get; set; }

    public long XMin { get; set; }

    public long XMax { get; set; }

    // NOTE: 0 is the original floor and Y increasing goes UP
    public long YCeiling { get; set; }

    public Shape CurrentShape { get; set; } = null!;

    public long GetPointer => pointer;

    public Dictionary<string, long> Occupied { get; set; }

    public bool MakesContact(List<Point> positions)
    {
        if (positions.Any(p => Occupied.ContainsKey(p.ToString())))
            return true;

        if (positions.Any(p => p.X <= XMin || p.X >= XMax || p.Y <= 0))
            return true;

        return false;
    }

    public void ComesToRest(List<Point> positions)
    {
        foreach (var position in positions)
        {
            if (!Occupied.ContainsKey(position.ToString()))
                Occupied.Add(position.ToString(), position.Y);
        }
    }

    public void PlayShapeOut()
    {
        // Create Shape
        CurrentShape = new Shape(NumberOfShapes % 5, XMin, XMax, YCeiling);

        // Print - Uncommented out if you need to see the Pit's layout
        //Print(shape);

        var turn = 0;
        var done = false;

        do
        {
            // Lateral
            if (turn % 2 == 0)
            {
                var inst = Instructions[pointer];

                var potential = CurrentShape.TryMoveLaterally(inst);
                if (!MakesContact(potential))
                {
                    CurrentShape.SetPositions(potential);
                }

                pointer++;
                if (pointer >= Instructions.Count)
                    pointer = 0;
            }
            // Down
            else
            {
                var potential = CurrentShape.TryMoveDown();
                if (!MakesContact(potential))
                {
                    CurrentShape.SetPositions(potential);
                }
                else
                {
                    done = true;
                    ComesToRest(CurrentShape.GetCurrentPositions);
                }
            }

            turn++;
        } while (!done);

        var greatestY = Occupied.Select(o => o.Value).Max();
        if (greatestY > YCeiling)
            YCeiling = greatestY;

        NumberOfShapes++;
    }

    public void Print(Shape shape)
    {
        var shapePositionsString = shape.GetCurrentPositions.Select(c => c.ToString());
        for (long y = YCeiling + 8; y > 0; y--)
        {
            var positionsString = shapePositionsString.ToList();
            var builder = new StringBuilder();
            for (long x = XMin + 1; x < XMax; x++)
            {
                var coordAsString = $"{x},{y}";
                if (positionsString.Contains(coordAsString))
                    builder.Append("@");
                else if (Occupied.ContainsKey(coordAsString))
                    builder.Append("#");
                else
                    builder.Append(".");
            }

            Debug.WriteLine(builder.ToString());
        }

        Debug.WriteLine("");
        Debug.WriteLine("");
        Debug.WriteLine("");
    }
}