using Models;

namespace Helpers;

public interface DataFactory
{
  Listing GetListing();
  Address GetAddress();
  Person GetPerson();
  List<Person> GetPeople(int numberOfPeople);
}