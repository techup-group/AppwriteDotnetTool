using AppwriteClient;
using AppwriteClient.DTOs;
using System.Text.Json;
using Helpers;
using Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConfigurationHelper configHelper = new ConfigurationHelper();
        AppwriteService appwriteService = new AppwriteService(configHelper.GetSettings());
        var databaseId = GetDatabaseIdOrExit(configHelper);
        var databaseResponse = await appwriteService.GetDatabase(databaseId);
        bool databaseExists = databaseResponse.Result is not null;

        Console.WriteLine($"Database '{databaseId}' {(databaseExists ? "does" : "does not ")} exist");

        await PromptForDatabaseReset(databaseExists, databaseId, appwriteService, databaseResponse);

        DatabaseSeeder dbSeeder = new DatabaseSeeder(new FakerFactory(), appwriteService, databaseId);

        await dbSeeder.SeedCollections(3);
    }

    private static string GetDatabaseIdOrExit(ConfigurationHelper configHelper)
    {
        var databaseId = configHelper.GetSetting("DATABASE_ID");
        if (string.IsNullOrEmpty(databaseId))
        {
            Console.WriteLine("Database ID is not set in the app settings.");
            Environment.Exit(1);
        }
        return databaseId;
    }

    private static async Task PromptForDatabaseReset(bool databaseExists, string databaseId, AppwriteService appwriteService, DatabaseResponse<Appwrite.Models.Database> databaseResponse)
    {
        string databasePrompt = $"Would you like to {(databaseExists ? "reset" : "create")} the database from the local schema definition?";
        var shouldResetDatabase = UserSelection.GetBooleanAnswer(databasePrompt);

        if (!shouldResetDatabase) return;

        if (databaseExists)
        {
            Console.WriteLine("Deleting existing database...");
            await appwriteService.DeleteDatabase(databaseId);
        }

        try
        {
            string json = File.ReadAllText(Path.Combine("DDL", "housingsearch.json"));
            SeedDatabaseDTO? seedDatabaseDTO = JsonSerializer.Deserialize<SeedDatabaseDTO>(json);

            if (seedDatabaseDTO is null)
            {
                throw new JsonException();
            }

            Console.WriteLine("Creating database...");
            await appwriteService.CreateDatabase(new DatabaseDTO { DatabaseId = databaseId, Name = seedDatabaseDTO.DatabaseName });

            Console.WriteLine("Creating collections...");
            await appwriteService.CreateCollections(databaseId, seedDatabaseDTO.Collections);

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

    private static async Task PromptForCollectionOperations(AppwriteService appwriteService, string databaseId, DatabaseResponse<Appwrite.Models.Database> databaseResponse)
    {
        var collectionListResponse = await appwriteService.GetCollections(databaseResponse.Result);
        var collectionListCount = collectionListResponse.Result.Total;

        Console.WriteLine($"Database '{databaseId}' has {collectionListCount} collection(s).");

        var shouldOperateOnCollection = UserSelection.GetBooleanAnswer("Would you like to operate on a collection?");

        if (!shouldOperateOnCollection) Environment.Exit(0);

        var collectionNames = collectionListResponse.Result.Collections.Select(c => c.Name).ToList();

        var selectedCollection = UserSelection.GetSelection(collectionNames, "Select a collection:");

        Console.WriteLine($"You selected: {selectedCollection}");
    }
}