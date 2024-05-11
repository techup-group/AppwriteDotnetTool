using System.Text.Json.Serialization;

namespace AppwriteClient.Models
{
    public class DatabaseListModel
    {
        [JsonPropertyName("total")]
        public long Total { get; set; }

        [JsonPropertyName("databases")]
        public List<DatabaseModel> Databases { get; set; }
    }
}
