using Helpers;

namespace HelpersTests;

public class DatabaseSeederTests
{
    DatabaseSeeder _databaseSeeder;
    public DatabaseSeederTests()
    {
        IDataFactory dataFactory = new FakerFactory();
        IBackendService appwriteService = new FakeAppwriteService();
        string databaseId = "";
        _databaseSeeder = new DatabaseSeeder(dataFactory, appwriteService, databaseId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public async void SeedCollections_ReturnsDictionaryWithEnumerableObjects(int value)
    {
        // Act
        var result = await _databaseSeeder.SeedCollections(value);

        // Assert
        Assert.Equal(value, ((IEnumerable<object>)result["person"]).Count());
        Assert.Equal(value, ((IEnumerable<object>)result["users"]).Count());
    }
}

class FakeAppwriteService : IBackendService
{
    public Task<object> CreateDocument(string databaseId, string collectionId, string itemJson)
    {
        return Task.FromResult<object>(new());
    }
}