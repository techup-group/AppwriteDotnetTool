using System.Text.Json;
using System.Text.Json.Serialization;
using AppwriteClient;
using Models;

namespace Helpers;

class DatabaseSeeder
{
  private DataFactory _dataFactory;
  private AppwriteService _appwriteService;
  private string _databaseId;
  private string _personCollectionId = "person";
  private string _addressCollectionId = "address";
  private string _listingCollectionId = "listings";
  private string _userCollectionId = "users";

  public DatabaseSeeder(DataFactory dataFactory, AppwriteService appwriteService, string databaseId)
  {
    _dataFactory = dataFactory;
    _appwriteService = appwriteService;
    _databaseId = databaseId;
  }

  public async Task<List<Person>> SeedPeople(int numberOfPeople)
  {
    List<Person> people = _dataFactory.GetPeople(numberOfPeople);
    List<Task> tasks = new();
    foreach (Person person in people)
    {
      string personJson = JsonSerializer.Serialize(person);
      tasks.Add(_appwriteService.CreateDocument(_databaseId, _personCollectionId, personJson));
    }
    await Task.WhenAll(tasks);
    return people;
  }

  public async Task<Person> SeedPerson()
  {
    Person person = _dataFactory.GetPerson();
    string personJson = JsonSerializer.Serialize(person);
    await _appwriteService.CreateDocument(_databaseId, _personCollectionId, personJson);
    return person;
  }

  public async Task<Address> SeedAddress()
  {
    Address address = _dataFactory.GetAddress();
    string addressJson = JsonSerializer.Serialize(address);
    await _appwriteService.CreateDocument(_databaseId, _addressCollectionId, addressJson);
    return address;
  }

  public async Task<Listing> SeedListing()
  {
    Listing listing = _dataFactory.GetListing();
    var options = new JsonSerializerOptions
    {
      Converters = { new JsonStringEnumConverter() },
    };
    string listingJson = JsonSerializer.Serialize(listing, options);
    await _appwriteService.CreateDocument(_databaseId, _listingCollectionId, listingJson);
    return listing;
  }

  public async Task<User> SeedUser()
  {
    User user = this._dataFactory.GetUser();
    var options = new JsonSerializerOptions
    {
      Converters = { new JsonStringEnumConverter() },
    };
    string userJson = JsonSerializer.Serialize(user, options);
    await _appwriteService.CreateDocument(_databaseId, _userCollectionId, userJson);
    return user;
  }

  public async Task<List<User>> SeedUsers(int numberOfUsers)
  {
    List<User> users = _dataFactory.GetUsers(numberOfUsers);
    List<Task> tasks = new();
    foreach (User user in users)
    {
      var options = new JsonSerializerOptions
      {
        Converters = { new JsonStringEnumConverter() },
      };
      string userJson = JsonSerializer.Serialize(user, options);
      tasks.Add(_appwriteService.CreateDocument(_databaseId, _userCollectionId, userJson));
    }
    await Task.WhenAll(tasks);
    return users;
  }

  public async Task<Dictionary<string, object>> SeedCollections(int numberOfRecords)
  {
    Dictionary<string, object> collectionItems = new()
    {
        { _personCollectionId, await SeedPeople(numberOfRecords) },
        { _userCollectionId, await SeedUsers(numberOfRecords) }
    };
    return collectionItems;
  }
}