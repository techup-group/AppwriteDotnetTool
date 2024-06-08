namespace Helpers;

public class CommandLineArgumentParser
{
  public static bool HasArgument(string[] args, string argumentName)
  {
    return args.Any(arg => arg.Equals(argumentName, StringComparison.OrdinalIgnoreCase));
  }

  public static string GetArgumentValue(string[] args, string argumentName)
  {
    var argument = args.FirstOrDefault(arg => arg.StartsWith(argumentName + "=", StringComparison.OrdinalIgnoreCase));
    return argument?.Substring(argumentName.Length + 1) ?? "";
  }
}