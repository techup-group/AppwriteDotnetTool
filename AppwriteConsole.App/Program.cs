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

        var continueResponse = UserSelection.GetBooleanAnswer($"Retrieve database '{databaseId}'?");

        if (!continueResponse)
        {
            Environment.Exit(0);
        }

        var database = await ExecuteOrExitOnError(appwriteService.GetDatabase(databaseId), $"Database '{databaseId}' does not exist.");

        var collectionList = await ExecuteOrExitOnError(appwriteService.GetCollections(database));

        var collectionListCount = collectionList.Total;

        if (collectionListCount == 0)
        {
            Console.WriteLine($"Database '{databaseId}' is empty.");
            return;
        }

        Console.WriteLine($"Database '{databaseId}' exists and has {collectionListCount} collection(s).");

        var operateOnCollectionResponse = UserSelection.GetBooleanAnswer("Would you like to operate on a collection?");

        if (!operateOnCollectionResponse)
        {
            return;
        }

        var collectionNames = collectionList.Collections.Select(c => c.Name).ToList();

        var selectedCollection = UserSelection.GetSelection(collectionNames, "Select a collection:");

        Console.WriteLine($"You selected: {selectedCollection}");

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
