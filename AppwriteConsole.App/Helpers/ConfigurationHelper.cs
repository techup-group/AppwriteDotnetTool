using Microsoft.Extensions.Configuration;

namespace Helpers;

class ConfigurationHelper
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the ConfigurationHelper class.
    /// </summary>
    /// <param name="environmentName">The name of the environment (e.g., Development, Staging, Production).</param>
    public ConfigurationHelper(string environmentName = "")
    {
        // Get the environment name from the parameter or from the ASPNETCORE_ENVIRONMENT environment variable
        var environment = string.IsNullOrEmpty(environmentName)
          ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
          : environmentName;

        // Create a new ConfigurationBuilder and configure it to read from appsettings.json and appsettings.{environment}.json files
        var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

        // Build the configuration
        _configuration = builder.Build();
    }

    /// <summary>
    /// Prints all the configuration settings to the console.
    /// </summary>
    public void PrintSettings()
    {
        foreach (var entry in _configuration.AsEnumerable())
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    /// <summary>
    /// Gets the value of a specific configuration setting.
    /// </summary>
    /// <param name="key">The key of the configuration setting.</param>
    /// <returns>The value of the configuration setting, or null if the key is not present.</returns>
    public string? GetSetting(string key)
    {
        return _configuration[key];
    }

    /// <summary>
    /// Gets the entire configuration.
    /// </summary>
    /// <returns>The entire configuration.</returns>
    public IConfiguration GetSettings()
    {
        return _configuration;
    }
}
