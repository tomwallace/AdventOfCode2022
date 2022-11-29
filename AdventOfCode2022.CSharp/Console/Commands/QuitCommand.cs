namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Exits the CLI
/// </summary>
public class QuitCommand : ICommand
{
    /// <inheritdoc />
    public void Execute()
    {
        Environment.Exit(0);
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}