using Models;

namespace Helpers;

public interface DataFactory
{
  Listing GetListing();
  List<Listing> GetListings(int numberOfListings);
  Address GetAddress();
  List<Address> GetAddresses(int numberOfAddresses);
  Person GetPerson();
  List<Person> GetPeople(int numberOfPeople);
  User GetUser();
  List<User> GetUsers(int numberOfUsers);
}