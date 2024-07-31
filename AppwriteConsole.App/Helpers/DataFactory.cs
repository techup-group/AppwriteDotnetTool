using Appwrite.Models;
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
  Models.User GetUser();
  List<Models.User> GetUsers(int numberOfUsers);
}