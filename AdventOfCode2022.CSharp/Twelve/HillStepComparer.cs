namespace AdventOfCode2022.CSharp.Twelve;

public class HillStepComparer : IEqualityComparer<HillStep>
{
    public bool Equals(HillStep c1, HillStep c2)
    {
        if (c2 == null && c1 == null)
            return true;
        else if (c1 == null | c2 == null)
            return false;
        else if (c1.ToString() == c2.ToString())
            return true;
        else
            return false;
    }

    public int GetHashCode(HillStep c)
    {
        return $"{c}".GetHashCode();
    }
}