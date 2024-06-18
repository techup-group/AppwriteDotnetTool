using AppwriteClient;
using AppwriteClient.DTOs;
using Microsoft.Extensions.Configuration;
// using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using static AppwriteClient.DTOs.AttributeDTO;
using Helpers;

internal class Program
{
    private static async Task Main(string[] args)
    {
        bool skipConfirmation = CommandLineArgumentParser.HasArgument(args, "--skip-confirmation");
        string environmentName = CommandLineArgumentParser.GetArgumentValue(args, "--environment");

        ConfigurationHelper configHelper = new ConfigurationHelper(environmentName);

        if (!skipConfirmation)
        {
            var confirm = UserSelection.GetBooleanAnswer("Would you like to confirm application settings?");
            if (confirm)
            {
                configHelper.PrintSettings();
            }
        }
        else
        {
            Console.WriteLine("Skipping confirmation of application settings.");
        }

        AppwriteService appwriteService = new AppwriteService(configHelper.GetSettings());

        var databaseId = configHelper.GetSetting("DATABASE_ID");

        if (string.IsNullOrEmpty(databaseId))
        {
            Console.WriteLine("Database ID is not set.");
            return;
        }

        var continueResponse = UserSelection.GetBooleanAnswer($"Retrieve database '{databaseId}'?");

        if (!continueResponse)
        {
            Environment.Exit(0);
        }

        var databaseResponse = await appwriteService.GetDatabase(databaseId);

        if (databaseResponse.Result == null)
        {
            Console.WriteLine($"Database '{databaseId}' does not exist.");
            var createDatabaseResponse = UserSelection.GetBooleanAnswer("Would you like to create a new database from the housing search JSON file?");

            if (!createDatabaseResponse)
            {
                return;
            }

            try
            {
                string json = File.ReadAllText(Path.Combine("DDL", "housingsearch.json"));
                SeedDatabaseDTO? seedDatabaseDTO = JsonSerializer.Deserialize<SeedDatabaseDTO>(json);

                if (seedDatabaseDTO == null)
                {
                    Console.WriteLine("The housing search JSON file is not valid or present within the 'DDL' directory.");
                    Environment.Exit(1);
                }

                Console.WriteLine("Creating database...");
                await appwriteService.CreateDatabase(new DatabaseDTO { DatabaseId = databaseId, Name = seedDatabaseDTO.DatabaseName });

                Console.WriteLine("Creating collections...");
                await appwriteService.CreateCollections(databaseId, seedDatabaseDTO.Collections);

                // Fetch the collections of the database after creation
                databaseResponse = await appwriteService.GetDatabase(databaseId);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The housing search JSON file is not valid or present within the 'DDL' directory.");
                Environment.Exit(1);
            }
            catch (JsonException)
            {
                Console.WriteLine("The housing search JSON file is not valid or present within the 'DDL' directory.");
                Environment.Exit(1);
            }

        }

        var collectionListResponse = await appwriteService.GetCollections(databaseResponse.Result);
        var collectionListCount = collectionListResponse.Result.Total;

        if (collectionListCount == 0)
        {
            Console.WriteLine($"Database '{databaseId}' is empty.");
            return;
        }

        Console.WriteLine($"Database '{databaseId}' exists and has {collectionListCount} collection(s).");



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

    /// <summary>
    /// Executes a task and exits the application if the task returns an error.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the task.</typeparam>
    /// <param name="task">The task to be executed.</param>
    /// <param name="exitMessage">The message to be displayed if the task returns an error. If not provided, a default error message will be displayed.</param>
    /// <returns>The result of the task if it is executed successfully.</returns>
    private static async Task<T> ExecuteOrExitOnError<T>(Task<DatabaseResponse<T>> task, string exitMessage = "")
    {
        var response = await task;
        if (response.Error != null)
        {
            string message = string.IsNullOrEmpty(exitMessage) ? $"Error: {response.Error}" : exitMessage;
            Console.WriteLine(message);
            Environment.Exit(1);
        }
        return response.Result;
    }
}

public class CollectionDTO
{
    public string Name { get; set; }
    public List<string> Attributes { get; set; } // Add this property
}