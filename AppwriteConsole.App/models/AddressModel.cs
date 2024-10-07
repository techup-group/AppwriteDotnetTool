using Newtonsoft.Json;

namespace Models;

public class Address
{
    public string Id { get; set; } = string.Empty;
    public string StreetAddress1 { get; set; } = string.Empty;
    public string? StreetAddress2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Zip { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    public static Address FromJson(Dictionary<string, object> data)
    {
        string json = JsonConvert.SerializeObject(data);
        Address address = JsonConvert.DeserializeObject<Address>(json);
        return address;
    }
}