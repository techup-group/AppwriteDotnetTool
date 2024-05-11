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
                var createdAttribute = await databaseClient.CreateRelationshipAttribute(databaseId, collectionId, attribute.RelatedCollectionId, attribute.Type, attribute.TwoWay, attribute.Key, attribute.TwoWayKey, attribute.OnDelete);
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
}
