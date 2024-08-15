using Newtonsoft.Json;

namespace Models;

public class User
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public List<Listing>? Listings { get; set; }

  public static User FromJson(Dictionary<string, object> data)
  {
    string json = JsonConvert.SerializeObject(data);
    User user = JsonConvert.DeserializeObject<User>(json);
    return user;
  }
}