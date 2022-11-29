namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Provides the current version of CLI, which comes from configuration
/// </summary>
public class VersionCommand : ICommand
{
    private readonly string _versionNumber;

    public VersionCommand(string versionNumber)
    {
        _versionNumber = versionNumber ?? throw new ArgumentNullException(nameof(versionNumber));
    }

    /// <inheritdoc />
    public void Execute()
    {
        System.Console.WriteLine($"AdventOfCode2020 version: {_versionNumber}");
        System.Console.WriteLine("");
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}