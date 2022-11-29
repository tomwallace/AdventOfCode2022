using System.Reflection;

namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Runs one of the IAdventProblemSets, as determined by the passed in parameters
/// </summary>
public class RunCommand : ICommand
{
    private readonly string _className = null!;
    private readonly string _methodName = null!;
    private readonly bool _errorInCreation;

    private readonly List<string> _allowedMethodNames = new List<string>() { "PartA", "PartB" };

    /// <summary>
    /// Constructor, as this command can only be created with an array of parameters
    /// </summary>
    /// <param name="commandSplit">The parameters used to run the command</param>
    public RunCommand(string[] commandSplit)
    {
        if (commandSplit.Length != 3)
        {
            _errorInCreation = true;
        }
        else
        {
            _className = commandSplit[1];
            _methodName = commandSplit[2];

            if (!_className.Contains("Day") || !_allowedMethodNames.Contains(_methodName))
                _errorInCreation = true;
        }
    }

    /// <inheritdoc />
    public void Execute()
    {
        var interfaceType = typeof(IAdventProblemSet);
        var problemSetTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && p.IsInterface == false);
        var problemSetType = problemSetTypes.FirstOrDefault(t => t.Name == _className);

        if (problemSetType == null)
        {
            System.Console.WriteLine("The problem set was not found.");
            System.Console.WriteLine("Have you entered a Day that has not occurred yet?");
            System.Console.WriteLine("");
            return;
        }

        var instance = Activator.CreateInstance(problemSetType);
        MethodInfo? method = problemSetType.GetMethod(_methodName);
        string output = (string)method?.Invoke(instance, Array.Empty<object>())!;

        System.Console.WriteLine($"The result is: {output}");
        System.Console.WriteLine("");
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return _errorInCreation;
    }
}