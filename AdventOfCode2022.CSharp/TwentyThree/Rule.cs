namespace AdventOfCode2022.CSharp.TwentyThree;

public class Rule
{
    public Rule(string name, Func<Elf, (int, int)?> op)
    {
        Name = name;
        Op = op;
    }

    public string Name { get; set; }

    public Func<Elf, (int, int)?> Op { get; set; }

    public override string ToString()
    {
        return Name;
    }
}