using Newtonsoft.Json;

namespace Models;

public class User
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public List<Listing> Listings { get; set; }

  public static User FromJson(Dictionary<string, object> data)
  {
    string json = JsonConvert.SerializeObject(data);
    User user = JsonConvert.DeserializeObject<User>(json);
    return user;
  }
}