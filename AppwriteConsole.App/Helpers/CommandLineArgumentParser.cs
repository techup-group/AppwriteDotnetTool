namespace Helpers;

public class CommandLineArgumentParser
{
    /// <summary>
    /// Checks if a specific argument is present in the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <param name="argumentName">The name of the argument to check for.</param>
    /// <returns>True if the argument is present, false otherwise.</returns>
    public static bool HasArgument(string[] args, string argumentName)
    {
        return args.Any(arg => arg.Equals(argumentName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the value of a specific argument from the command-line arguments.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    /// <param name="argumentName">The name of the argument to retrieve the value for.</param>
    /// <returns>The value of the argument, or an empty string if the argument is not present.</returns>
    public static string GetArgumentValue(string[] args, string argumentName)
    {
        var argument = args.FirstOrDefault(arg => arg.StartsWith(argumentName + "=", StringComparison.OrdinalIgnoreCase));
        return argument?.Substring(argumentName.Length + 1) ?? "";
    }
}