using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Models;

public class Person
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string AccountId { get; set; }
  public Address AddressId { get; set; }
  public static Person FromJson(Dictionary<string, object> data)
  {
    string json = JsonConvert.SerializeObject(data);
    Person person = JsonConvert.DeserializeObject<Person>(json);
    return person;
  }
}