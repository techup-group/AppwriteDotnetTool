using System.Text.Json.Serialization;

namespace AppwriteClient.Models
{
    public class CollectionModel
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("$createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("$updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("$permissions")]
        public List<string> Permissions { get; set; }

        [JsonPropertyName("databaseId")]
        public string DatabaseId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("documentSecurity")]
        public bool DocumentSecurity { get; set; }

        [JsonPropertyName("attributes")]
        public List<Attribute> Attributes { get; set; }

        [JsonPropertyName("indexes")]
        public List<Index> Indexes { get; set; }
    }

    public class Index
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("attributes")]
        public List<object> Attributes { get; set; }

        [JsonPropertyName("orders")]
        public List<object> Orders { get; set; }
    }
}
