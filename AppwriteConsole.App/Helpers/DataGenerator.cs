using System.Text.Json;
using Models;

namespace Helpers;

class DataGenerator
{
  private DataFactory DataFactory;

  public DataGenerator(DataFactory dataFactory)
  {
    this.DataFactory = dataFactory;
  }

  public string GetSerializedPeople(int numberOfPeople)
  {
    List<Person> people = this.DataFactory.GetPeople(numberOfPeople);
    return JsonSerializer.Serialize(people);
  }
}