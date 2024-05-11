using AppwriteClient;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var setting1 = configuration["DATABASE_ID"];
        var setting2 = configuration["PROJECT_KEY"];

        Console.WriteLine($"Setting1: {setting1}");
        Console.WriteLine($"Setting2: {setting2}");

        AppwriteService _service = new(configuration);

        /*
        * TODO: Validate that a database doesnt already exist
        * TODO: Create Backup
        * TODO: Create Database
        * TODO: Create Collections and Attributes based on file schema
        * TODO: Add logging for process
        * TODO: Add required user prompts
        * TODO: Write unit test for code
        */
    }
}