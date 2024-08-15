using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Models;

public class Person
{
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public string AccountId { get; set; } = string.Empty;
  public Address? AddressId { get; set; }
  public static Person FromJson(Dictionary<string, object> data)
  {
    string json = JsonConvert.SerializeObject(data);
    Person person = JsonConvert.DeserializeObject<Person>(json);
    return person;
  }
}