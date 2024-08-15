namespace Helpers;

public interface IBackendService
{
    public Task<object> CreateDocument(string databaseId, string collectionId, string itemJson);
}