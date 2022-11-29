namespace AdventOfCode2022.CSharp;

/// <summary>
/// Interface that defines the Advent of Code problem sets
/// </summary>
public interface IAdventProblemSet
{
    /// <summary>
    /// Solves Part A of the ProblemSet and provides the answer back as a string
    /// </summary>
    /// <returns>The answer for Part A</returns>
    string PartA();

    /// <summary>
    /// Solves Part B of the ProblemSet and provides the answer back as a string
    /// </summary>
    /// <returns>The answer for Part B</returns>
    string PartB();

    /// <summary>
    /// Description of the ProblemSet, which is used in the CLI list command to provide context.  Add [HARD] at end to signify the ProblemSets struggled with most.
    /// </summary>
    /// <returns>The ProblemSet description</returns>
    string Description();

    /// <summary>
    /// The sort order of the ProblemSet, used to display the problems in the correct order with the CLI list command
    /// </summary>
    /// <returns>The ProblemSet sort order</returns>
    int SortOrder();
}