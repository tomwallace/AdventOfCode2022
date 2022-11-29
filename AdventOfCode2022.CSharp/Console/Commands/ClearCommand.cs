namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Clears the current CLI screen
/// </summary>
public class ClearCommand : ICommand
{
    /// <inheritdoc />
    public void Execute()
    {
        System.Console.Clear();
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}