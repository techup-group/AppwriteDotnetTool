using Newtonsoft.Json;

namespace Models;

public class Address
{
  public string Id { get; set; }
  public string StreetAddress1 { get; set; }
  public string? StreetAddress2 { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Zip { get; set; }
  public string County { get; set; }
  public static Address FromJson(Dictionary<string, object> data)
  {
    string json = JsonConvert.SerializeObject(data);
    Address address = JsonConvert.DeserializeObject<Address>(json);
    return address;
  }
}