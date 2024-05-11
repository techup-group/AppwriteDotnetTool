using System.Text.Json.Serialization;

namespace AppwriteClient.DTOs
{
    public class DatabaseDTO
    {
        [JsonPropertyName("databaseId")]
        public string DatabaseId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
