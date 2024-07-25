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

    // Address address = (data["AddressId"] as JObject).ToObject<Address>();
    // var person = new Person
    // {
    //   FirstName = data["FirstName"].ToString(),
    //   LastName = data["LastName"].ToString(),
    //   Email = data["Email"].ToString(),
    //   Phone = data["Phone"].ToString(),
    //   AccountId = data["AccountId"].ToString(),
    //   AddressId = address
    // };

    return person;
  }
}