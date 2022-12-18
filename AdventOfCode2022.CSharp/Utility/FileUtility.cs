namespace AdventOfCode2022.CSharp.Utility;

public static class FileUtility
{
    /// <summary>
    /// Splits a file into values of type T by carriage return, using a defined parser function.
    /// Returns the values as a List of type T
    /// </summary>
    /// <typeparam name="T">The type to cast each line into</typeparam>
    /// <param name="filePath">The path of the file, relative to the root of the main project</param>
    /// <param name="parser">The parser function used to cast each row into type T</param>
    /// <returns>A List of type T from each line of the file</returns>
    public static List<T> ParseFileToList<T>(string filePath, Func<string, T> parser)
    {
        List<T> splits = new List<T>();
        StreamReader file = new StreamReader(filePath);

        // Iterate over each line in the input
        while (file.ReadLine()! is { } line)
        {
            splits.Add(parser(line));
        }
        file.Close();
        return splits;
    }

    /// <summary>
    /// Overload that assumes the types returned will be strings, given no parser was provided
    /// </summary>
    /// <param name="filePath">The path of the file, relative to the root of the main project</param>
    /// <returns>A List of string of each line of the file</returns>
    public static List<string> ParseFileToList(string filePath)
    {
        List<string> splits = new List<string>();
        StreamReader file = new StreamReader(filePath);

        // Iterate over each line in the input
        while (file.ReadLine()! is { } line)
        {
            splits.Add(line);
        }
        file.Close();
        return splits;
    }

    /// <summary>
    /// Splits a file into lines by carriage return, then creates a dictionary based on the defined
    /// parser function, with Item1 creating the index and Item2 creating the value.
    /// Returns the dictionary.
    /// </summary>
    /// <typeparam name="I">Type for the dictionary index</typeparam>
    /// <typeparam name="T">Type for the dictionary value</typeparam>
    /// <param name="filePath">The path of the file, relative to the root of the main project</param>
    /// <param name="parser">The parser functions used to create the dictionary</param>
    /// <returns>A Dictionary of type I,T from each line of the file</returns>
    public static Dictionary<I, T> ParseFileToDictionary<I, T>(string filePath, (Func<string, I>, Func<string, T>) parser) where I : notnull
    {
        Dictionary<I, T> results = new Dictionary<I, T>();
        StreamReader file = new StreamReader(filePath);

        // Iterate over each line in the input
        while (file.ReadLine()! is { } line)
        {
            results.Add(parser.Item1(line), parser.Item2(line));
        }
        file.Close();
        return results;
    }
}