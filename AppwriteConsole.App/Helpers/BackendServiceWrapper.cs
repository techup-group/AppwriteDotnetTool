using Helpers;
using AppwriteClient;

namespace Helpers;

class AppwriteServiceWrapper : IBackendService
{
    private AppwriteService _appwriteService;

    public AppwriteServiceWrapper(AppwriteService appwriteService)
    {
        _appwriteService = appwriteService;
    }

    public async Task<object> CreateDocument(string databaseId, string collectionId, string itemJson)
    {
        var result = await _appwriteService.CreateDocument(databaseId, collectionId, itemJson);
        return result;
    }
}