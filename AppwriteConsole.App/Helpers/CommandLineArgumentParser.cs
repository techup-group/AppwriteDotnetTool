namespace Helpers;

public class CommandLineArgumentParser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <param name="argumentName"></param>
    /// <returns></returns>
    public static bool HasArgument(string[] args, string argumentName)
    {
        return args.Any(arg => arg.Equals(argumentName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <param name="argumentName"></param>
    /// <returns></returns>
    public static string GetArgumentValue(string[] args, string argumentName)
    {
        var argument = args.FirstOrDefault(arg => arg.StartsWith(argumentName + "=", StringComparison.OrdinalIgnoreCase));
        return argument?.Substring(argumentName.Length + 1) ?? "";
    }
}