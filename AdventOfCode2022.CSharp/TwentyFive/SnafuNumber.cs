namespace AdventOfCode2022.CSharp.TwentyFive;

public class SnafuNumber
{
    // ex: 1=11-2
    public SnafuNumber(string input)
    {
        Original = input;
    }

    public SnafuNumber(long input)
    {
        Original = ToSnafu(input);
    }

    public string Original { get; set; }

    public override string ToString()
    {
        return Original;
    }

    public long ToDecimal()
    {
        var chars = Original.ToCharArray().Reverse().ToList();
        long total = 0;

        for (int i = 0; i < chars.Count(); i++)
        {
            var multiplier = i == 0 ? 1 : (long)Math.Pow(5, i);
            var current = chars[i];
            if (char.IsDigit(current))
                total += multiplier * long.Parse(current.ToString());
            else
            {
                switch (current)
                {
                    case '=':
                        total += multiplier * -2;
                        break;

                    case '-':
                        total += multiplier * -1;
                        break;

                    default:
                        throw new ArgumentException($"Unrecognized value {current}");
                }
            }
        }

        return total;
    }

    private string ToSnafu(long input)
    {
        var current = input;
        var output = string.Empty;
        while (current > 0)
        {
            var result = RecurseSnafu(current);
            output = $"{result.Item1}{output}";
            current = result.Item2;
        }

        return output;
    }

    private (char, long) RecurseSnafu(long input)
    {
        if (input == 0)
            return ('0', 0);

        var remainder = input % 5;
        var divided = input / 5;

        if (remainder > 2)
        {
            divided++;
            remainder -= 5;
        }

        switch (remainder)
        {
            case -2:
                return ('=', divided);

            case -1:
                return ('-', divided);

            case 0:
            case 1:
            case 2:
                return (remainder.ToString()[0], divided);

            default:
                throw new ArgumentException($"Unrecognized value {remainder}");
        }
    }
}