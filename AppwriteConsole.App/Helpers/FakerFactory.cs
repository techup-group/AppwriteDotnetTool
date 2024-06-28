using Faker;
using FakerBoolean = Faker.Boolean;
using Models;

public static class FakerFactory
{
  public static Listing GenerateListing()
  {
    var listing = new Listing
    {
      Title = Lorem.Sentence(3),
      Description = Lorem.Paragraph(2),
      Location = Address.City(),
      Price = RandomNumber.Next(0, 10001), // Assuming Price is an integer
      Bedrooms = RandomNumber.Next(1, 11), // Assuming Bedrooms is an integer
      PetsAllowed = FakerBoolean.Random(),
      ParkingAvailable = FakerBoolean.Random(),
      AvailableDate = DateTime.Now.AddDays(RandomNumber.Next(0, 365)).ToString("yyyy-MM-dd"),
      HousingType = new[] { "Apt", "House" }[RandomNumber.Next(0, 1)] // Randomly selects between "Apt" and "House"
    };

    return listing;
  }
}