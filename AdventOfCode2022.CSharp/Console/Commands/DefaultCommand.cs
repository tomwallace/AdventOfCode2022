namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Default command, which is used if the CLI command input is not recognized
/// </summary>
public class DefaultCommand : ICommand
{
    /// <inheritdoc />
    public void Execute()
    {
        System.Console.WriteLine("Command not recognized.  Type /help or /h for available commands.");
        System.Console.WriteLine("");
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}