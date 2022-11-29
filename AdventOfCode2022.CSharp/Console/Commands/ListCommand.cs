namespace AdventOfCode2022.CSharp.Console.Commands;

/// <summary>
/// Lists out the registered IAdventProblemSets in the correct order
/// </summary>
public class ListCommand : ICommand
{
    /// <inheritdoc />
    public void Execute()
    {
        var interfaceType = typeof(IAdventProblemSet);
        var problemSetTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => interfaceType.IsAssignableFrom(p) && p.IsInterface == false)
            .OrderBy(problemSetType =>
            {
                IAdventProblemSet? instance = (IAdventProblemSet)Activator.CreateInstance(problemSetType, null)!;
                return instance?.SortOrder();
            });

        System.Console.WriteLine("AdventOfCode2020 has the following problem sets:");
        System.Console.WriteLine("");

        System.Console.WriteLine("Class Name  -  Description");
        System.Console.WriteLine("-----------------------------------------------------");

        foreach (var problemSetType in problemSetTypes)
        {
            IAdventProblemSet? instance = (IAdventProblemSet)Activator.CreateInstance(problemSetType, null)!;
            System.Console.WriteLine($"{problemSetType.Name}  -  {instance.Description()}");
        }

        System.Console.WriteLine("");
    }

    /// <inheritdoc />
    public bool HadErrorInCreation()
    {
        return false;
    }
}