using System.Net;
using Appwrite;
using Appwrite.Enums;
using Appwrite.Models;
using Appwrite.Services;
using AppwriteClient.DTOs;
using Microsoft.Extensions.Configuration;
using static AppwriteClient.DTOs.AttributeDTO;

namespace AppwriteClient
{
    public class AppwriteService
    {
        private readonly Databases databaseClient;
        private readonly IConfiguration _config;
        public AppwriteService(IConfiguration config)
        {
            var _client = new Client();
            _config = config;
            _client.SetEndpoint("https://cloud.appwrite.io/v1")
                   .SetProject(_config["PROJECT_KEY"])
                   .SetKey(_config["APPWRITE_KEY"]);
            databaseClient = new Databases(_client);
        }

        public async Task CreateDatabase(DatabaseDTO databaseDTO)
        {
            try
            {
                await databaseClient.Create(databaseDTO.DatabaseId, databaseDTO.Name, false);
            }
            catch (AppwriteException ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a database by its ID.
        /// </summary>
        /// <param name="databaseId">The ID of the database to retrieve.</param>
        /// <returns>A <see cref="DatabaseResponse{Database}"/> object containing the retrieved database or an error message.</returns>
        public async Task<DatabaseResponse<Database>> GetDatabase(string databaseId)
        {
            try
            {
                var result = await databaseClient.Get(databaseId);
                return new DatabaseResponse<Database> { Result = result, Error = null };
            }
            catch (AppwriteException ex)
            {
                return new DatabaseResponse<Database> { Result = null, Error = ex.Message };
            }
        }

        public async Task<DatabaseResponse<CollectionList>> GetCollections(string databaseId)
        {
            try
            {
                var result = await databaseClient.ListCollections(databaseId);
                return new DatabaseResponse<CollectionList> { Result = result, Error = null };
            }
            catch (AppwriteException ex)
            {
                return new DatabaseResponse<CollectionList> { Result = null, Error = ex.Message };
            }
        }

        public async Task<DatabaseResponse<CollectionList>> GetCollections(Database database)
        {
            return await GetCollections(database.Id);
        }

        public async Task CreateCollections(string databaseId, List<CollectionDTO> collectionList)
        {
            foreach (var collection in collectionList)
            {
                await databaseClient.CreateCollection(databaseId, collection.CollectionId, collection.Name);
                await CreateCollectionAttributes(databaseId, collection.CollectionId, collection);
            }
        }

        /// <summary>
        /// Creates the attributes for a collection in the Appwrite database.
        /// </summary>
        /// <param name="databaseId">The ID of the database.</param>
        /// <param name="collectionId">The ID of the collection.</param>
        /// <param name="collection">The collection DTO object.</param>
        private async Task CreateCollectionAttributes(string databaseId, string collectionId, CollectionDTO collection)
        {
            var attributeMap = new Dictionary<Type, Func<Task>>
    {
        { typeof(StringAttribute), () => CreateAttribute(collection.StringAttributes, databaseId, collectionId) },
        { typeof(EmailAttribute), () => CreateAttribute(collection.EmailAttributes, databaseId, collectionId) },
        { typeof(EnumAttribute), () => CreateAttribute(collection.EnumAttributes, databaseId, collectionId) },
        { typeof(IPAddressAttribute), () => CreateAttribute(collection.IPAddressAttributes, databaseId, collectionId) },
        { typeof(URLAttribute), () => CreateAttribute(collection.URLAttributes, databaseId, collectionId) },
        { typeof(IntegerAttribute), () => CreateAttribute(collection.IntegerAttributes, databaseId, collectionId) },
        { typeof(FloatAttribute), () => CreateAttribute(collection.FloatAttributes, databaseId, collectionId) },
        { typeof(BooleanAttribute), () => CreateAttribute(collection.BooleanAttributes, databaseId, collectionId) },
        { typeof(DatetimeAttribute), () => CreateAttribute(collection.DatetimeAttributes, databaseId, collectionId) },
        { typeof(RelationshipAttribute), () => CreateAttribute(collection.RelationshipAttributes, databaseId, collectionId) },
    };

            foreach (var attributeType in attributeMap.Keys)
            {
                // Get the properties of the collection object that are of type List<T>
                var listProperties = collection.GetType().GetProperties()
                    .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>));

                // Filter the properties to only those where the generic type argument is the current attribute type
                var attributeListProperty = listProperties
                    .Where(p => p.PropertyType.GenericTypeArguments[0] == attributeType)
                    .FirstOrDefault();

                // Get the value of the property (which is a list of attribute objects)
                var attributeList = attributeListProperty?.GetValue(collection) as IEnumerable<object>;

                // If the list of attributes is not null or empty, create the attributes in the Appwrite database
                if (attributeList != null && attributeList.Any())
                {
                    await attributeMap[attributeType]();
                }
            }
        }

        public async Task CreateAttribute(List<StringAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateStringAttribute(databaseId, collectionId, attribute.Key, attribute.Size, attribute.Required, attribute.Default, attribute.Array, attribute.Encrypt);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);
            }

        }

        public async Task CreateAttribute(List<EmailAttribute> attributeList, string databaseId, string collectionId)
        {
            List<AttributeEmail> attributes = new List<AttributeEmail>();
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateEmailAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);

            }

        }

        public async Task CreateAttribute(List<IntegerAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateIntegerAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Min, attribute.Max, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);
            }
        }

        public async Task CreateAttribute(List<URLAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateUrlAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);

            }
        }

        public async Task CreateAttribute(List<IPAddressAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateIpAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);

            }

        }

        public async Task CreateAttribute(List<EnumAttribute> attributeList, string databaseId, string collectionId)
        {

            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateEnumAttribute(databaseId, collectionId, attribute.Key, attribute.Elements, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);

            }
        }

        public async Task CreateAttribute(List<RelationshipAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateRelationshipAttribute(databaseId, collectionId, attribute.RelatedCollectionId, RelationshipAttribute.ToRelationsipType(attribute.Type), attribute.TwoWay, attribute.Key, attribute.TwoWayKey, RelationshipAttribute.ToRelationMutate(attribute.OnDelete));
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);
            }

        }

        public async Task CreateAttribute(List<BooleanAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateBooleanAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);

            }

        }

        public async Task CreateAttribute(List<DatetimeAttribute> attributeList, string databaseId, string collectionId)
        {

            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateDatetimeAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);
            }
        }

        public async Task CreateAttribute(List<FloatAttribute> attributeList, string databaseId, string collectionId)
        {
            foreach (var attribute in attributeList)
            {
                var createdAttribute = await databaseClient.CreateFloatAttribute(databaseId, collectionId, attribute.Key, attribute.Required, attribute.Min, attribute.Max, attribute.Default, attribute.Array);
                Console.WriteLine("Created Attribute: " + createdAttribute.Key);
            }
        }

        public async Task<CollectionList> GetCollectionList(string databaseId)
        {
            var collectionList = await databaseClient.ListCollections(databaseId);
            return collectionList;
        }

        public async Task<DocumentList> GetDocuments(string databaseId, string collectionId)
        {
            try
            {
                var documents = await databaseClient.ListDocuments(databaseId, collectionId);
                return documents;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //public async Task GetDocuments(string databaseId, string collectionId)
        //{
        //    List<string> queries = new List<string>();
        //    queries.Add("Query.notEqual(FirstName,[Fra])");
        //    try
        //    {
        //        var documents = await databaseClient.ListDocuments(databaseId, collectionId, queries);
        //        foreach (var document in documents.Documents)
        //        {
        //            Console.WriteLine("Document ID: " + document.Id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
    }

    /// <summary>
    /// Represents a response from a database operation, containing either a result or an error message.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    public class DatabaseResponse<T>
    {
        /// <summary>
        /// The result of the database operation, or null if an error occurred.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// The error message if an error occurred, or null if the operation was successful.
        /// </summary>
        public string Error { get; set; }
    }
}
