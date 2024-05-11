using System.Text.Json.Serialization;

namespace AppwriteClient.Models
{
    public class DatabaseModel
    {
        [JsonPropertyName("$id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("$createdAt")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("$updatedAt")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
    }
}
