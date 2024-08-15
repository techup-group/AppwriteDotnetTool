using Newtonsoft.Json;

namespace Models
{
    public class Listing
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Bedrooms { get; set; }
        public bool PetsAllowed { get; set; }
        public bool ParkingAvailable { get; set; }
        public DateTime AvailableDate { get; set; }
        public HousingType HousingType { get; set; }

        public static Listing FromJson(Dictionary<string, object> data)
        {
            string json = JsonConvert.SerializeObject(data);
            Listing listing = JsonConvert.DeserializeObject<Listing>(json);
            return listing;
        }
    }
}

public enum HousingType
{
    Apt,
    House,
}