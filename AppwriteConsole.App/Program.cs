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
    private static async Task Main()
    {
        ConfigurationHelper configHelper = new ConfigurationHelper();

        AppwriteService appwriteService = new AppwriteService(configHelper.GetSettings());

        var databaseResponse = await appwriteService.GetDatabase(configHelper.GetSetting("DATABASE_ID"));
        // // var response = await appwriteService.GetDatabase("INVALID_ID");

        if (databaseResponse.Error != null)
        {
            Console.WriteLine($"Error: {databaseResponse.Error}");
            Environment.Exit(1);
        }

        var database = databaseResponse.Result;

        var collectionsResponse = await appwriteService.GetCollections(database);

        if (collectionsResponse.Error != null)
        {
            Console.WriteLine($"Error: {collectionsResponse.Error}");
            Environment.Exit(1);
        }

        Console.WriteLine($"Total collections: {collectionsResponse.Result.Total}");

        foreach (var collection in collectionsResponse.Result.Collections)
        {
            Console.WriteLine($"Collection ID: {collection.Id}, Name: {collection.Name}");
        }

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