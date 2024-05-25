using Microsoft.Extensions.Configuration;

namespace Helpers;

class ConfigurationHelper
{
  private readonly IConfiguration _configuration;

  public ConfigurationHelper(string environmentName = "")
  {
    var environment = string.IsNullOrEmpty(environmentName)
      ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
      : environmentName;
    var builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
    _configuration = builder.Build();
  }
  public void PrintSettings()
  {
    foreach (var entry in _configuration.AsEnumerable())
    {
      Console.WriteLine($"{entry.Key}: {entry.Value}");
    }
  }
  public string GetSetting(string key)
  {
    return _configuration[key];
  }
  public IConfiguration GetSettings()
  {
    return _configuration;
  }
}



