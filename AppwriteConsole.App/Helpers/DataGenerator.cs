using System.Text.Json;
using System.Text.Json.Serialization;
using AppwriteClient;
using Models;

namespace Helpers;

/// <summary>
/// The <c>DatabaseSeeder</c> class is responsible for seeding various collections in the database.
/// It uses a <see cref="DataFactory"/> to generate data and an <see cref="AppwriteService"/> to interact with the Appwrite backend.
/// </summary>
class DatabaseSeeder
{
  private DataFactory _dataFactory;
  private AppwriteService _appwriteService;
  private string _databaseId;
  private string _personCollectionId = "person";
  private string _addressCollectionId = "address";
  private string _listingCollectionId = "listings";
  private string _userCollectionId = "users";

  /// <summary>
  /// Initializes a new instance of the <see cref="DatabaseSeeder"/> class.
  /// </summary>
  /// <param name="dataFactory">The data factory used to generate data.</param>
  /// <param name="appwriteService">The Appwrite service used to interact with the backend.</param>
  /// <param name="databaseId">The ID of the database to seed.</param>
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
      Task task = SerializeAndCreate(person, _personCollectionId);
      tasks.Add(task);
    }
    await Task.WhenAll(tasks);
    return people;
  }

  private Task<Appwrite.Models.Document> SerializeAndCreate(object item, string collectionId, JsonSerializerOptions? serializationOptions = null)
  {
    string itemJson = JsonSerializer.Serialize(item, serializationOptions);
    return _appwriteService.CreateDocument(_databaseId, collectionId, itemJson);
  }

  public async Task<Person> SeedPerson()
  {
    Person person = _dataFactory.GetPerson();
    await SerializeAndCreate(person, _personCollectionId);
    return person;
  }

  public async Task<Address> SeedAddress()
  {
    Address address = _dataFactory.GetAddress();
    await SerializeAndCreate(address, _addressCollectionId);
    return address;
  }

  public async Task<Listing> SeedListing()
  {
    Listing listing = _dataFactory.GetListing();
    var options = new JsonSerializerOptions
    {
      Converters = { new JsonStringEnumConverter() },
    };
    await SerializeAndCreate(listing, _listingCollectionId, options);
    return listing;
  }

  public async Task<User> SeedUser()
  {
    User user = this._dataFactory.GetUser();
    var options = new JsonSerializerOptions
    {
      Converters = { new JsonStringEnumConverter() },
    };
    await SerializeAndCreate(user, _userCollectionId, options);
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
      Task task = SerializeAndCreate(user, _userCollectionId, options);
      tasks.Add(task);
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