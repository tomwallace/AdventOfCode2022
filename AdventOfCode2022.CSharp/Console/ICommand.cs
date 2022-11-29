namespace AdventOfCode2022.CSharp.Console;

/// <summary>
/// Interface that all commands used in the console application must implement
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Executes the command
    /// </summary>
    void Execute();

    /// <summary>
    /// Indication if there was an error in the creation of the command
    /// </summary>
    /// <returns>Boolean indicating if there was an error</returns>
    bool HadErrorInCreation();
}