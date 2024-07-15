using Faker;
using FakerBoolean = Faker.Boolean;
using FakerAddress = Faker.Address;
using Models;

public static class FakerFactory
{
  public static Listing GetListing()
  {
    var listing = new Listing
    {
      Title = Lorem.Sentence(3),
      Description = Lorem.Paragraph(2),
      Location = FakerAddress.City(),
      Price = RandomNumber.Next(0, 10001), // Assuming Price is an integer
      Bedrooms = RandomNumber.Next(1, 11), // Assuming Bedrooms is an integer
      PetsAllowed = FakerBoolean.Random(),
      ParkingAvailable = FakerBoolean.Random(),
      AvailableDate = DateTime.Now.AddDays(RandomNumber.Next(0, 365)).ToString("yyyy-MM-dd"),
      HousingType = new[] { "Apt", "House" }[RandomNumber.Next(0, 1)] // Randomly selects between "Apt" and "House"
    };

    return listing;
  }

  public static Models.Address GetAddress()
  {
    var address = new Models.Address
    {
      Id = Guid.NewGuid().ToString(),
      StreetAddress1 = FakerAddress.StreetAddress(),
      StreetAddress2 = RandomNumber.Next(0, 1) == 0 ? FakerAddress.SecondaryAddress() : null,
      City = FakerAddress.City(),
      State = FakerAddress.UsState(),
      Zip = FakerAddress.ZipCode(),
      County = FakerAddress.UkCounty(),
    };

    return address;
  }

  public static Person GetPerson()
  {
    var person = new Person
    {
      FirstName = Name.First(),
      LastName = Name.Last(),
      Email = Internet.Email(),
      Phone = Phone.Number(),
      AccountId = Guid.NewGuid().ToString(),
      AddressId = GetAddress()
    };

    return person;
  }

  public static List<Person> GetPeople(int numberOfPeople)
  {
    List<Person> people = new List<Person>();

    for (int i = 0; i < numberOfPeople; i++)
    {
      people.Add(GetPerson());
    }

    return people;
  }
}