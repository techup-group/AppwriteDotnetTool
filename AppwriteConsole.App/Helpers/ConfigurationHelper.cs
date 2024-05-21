using Microsoft.Extensions.Configuration;

namespace Helpers;

class ConfigurationHelper
{
  public IConfiguration Configuration { get; private set; }

  public ConfigurationHelper(string environmentName = "")
  {
    var environment = string.IsNullOrEmpty(environmentName)
      ? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
      : environmentName;
    var builder = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
    Configuration = builder.Build();
  }
  public void PrintConfiguration()
  {
    foreach (var entry in Configuration.AsEnumerable())
    {
      Console.WriteLine($"{entry.Key}: {entry.Value}");
    }
  }
  public string GetValue(string key)
  {
    return Configuration[key];
  }
}



