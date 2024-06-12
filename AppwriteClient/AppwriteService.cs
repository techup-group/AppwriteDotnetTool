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
                await CreateCollectionAttributes(databaseId, collection.CollectionId, collection.StringAttributes, collection.EmailAttributes, collection.EnumAttributes, collection.IPAddressAttributes, collection.URLAttributes, collection.IntegerAttributes, collection.FloatAttributes, collection.BooleanAttributes, collection.DatetimeAttributes, collection.RelationshipAttributes);
            }
        }

        private async Task CreateCollectionAttributes(string databaseId, string collectionId, List<StringAttribute> stringAttributes, List<EmailAttribute> emailAttributes, List<EnumAttribute> enumAttributes, List<IPAddressAttribute> iPAddressAttributes, List<URLAttribute> uRLAttributes, List<IntegerAttribute> integerAttributes, List<FloatAttribute> floatAttributes, List<BooleanAttribute> booleanAttributes, List<DatetimeAttribute> datetimeAttributes, List<RelationshipAttribute> relationshipAttributes)
        {

            if (stringAttributes != null && stringAttributes.Count > 0)
            {
                await CreateAttribute(stringAttributes, databaseId, collectionId);
            }

            if (emailAttributes != null && emailAttributes.Count > 0)
            {
                await CreateAttribute(emailAttributes, databaseId, collectionId);
            }

            if (enumAttributes != null && enumAttributes.Count > 0)
            {
                await CreateAttribute(enumAttributes, databaseId, collectionId);
            }

            if (iPAddressAttributes != null && iPAddressAttributes.Count > 0)
            {
                await CreateAttribute(iPAddressAttributes, databaseId, collectionId);
            }

            if (uRLAttributes != null && uRLAttributes.Count > 0)
            {
                await CreateAttribute(uRLAttributes, databaseId, collectionId);
            }

            if (integerAttributes != null && integerAttributes.Count > 0)
            {
                await CreateAttribute(integerAttributes, databaseId, collectionId);
            }

            if (floatAttributes != null && floatAttributes.Count > 0)
            {
                await CreateAttribute(floatAttributes, databaseId, collectionId);
            }

            if (booleanAttributes != null && booleanAttributes.Count > 0)
            {
                await CreateAttribute(booleanAttributes, databaseId, collectionId);
            }

            if (datetimeAttributes != null && datetimeAttributes.Count > 0)
            {
                await CreateAttribute(datetimeAttributes, databaseId, collectionId);
            }

            if (relationshipAttributes != null && relationshipAttributes.Count > 0)
            {
                await CreateAttribute(relationshipAttributes, databaseId, collectionId);
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
