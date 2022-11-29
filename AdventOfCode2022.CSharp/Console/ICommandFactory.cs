namespace AdventOfCode2022.CSharp.Console;

public interface ICommandFactory
{
    /// <summary>
    /// Builds a command based on the input from the CLI
    /// </summary>
    /// <param name="command">CLI command used to pick the appropriate command to create, as well as parsing out any passed arguments</param>
    /// <returns>The created command</returns>
    ICommand Build(string command);
}