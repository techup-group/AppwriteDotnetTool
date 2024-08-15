using Models;

namespace HelpersTests;

public class FakerFactoryTests
{
  FakerFactory _fakerFactory;

  public FakerFactoryTests()
  {
    _fakerFactory = new FakerFactory();
  }

  [Fact]
  public void GetListing_ReturnsValidListing()
  {
    // Arrange
    const int MIN_PRICE = 0;
    const int MAX_PRICE = 1000000;
    const int MIN_BEDROOMS = 1;
    const int MAX_BEDROOMS = 10;

    // Act
    Listing listing = _fakerFactory.GetListing();

    // Assert
    Assert.NotNull(listing);
    Assert.False(string.IsNullOrEmpty(listing.Title));
    Assert.False(string.IsNullOrEmpty(listing.Description));
    Assert.False(string.IsNullOrEmpty(listing.Location));
    Assert.True(listing.Price >= MIN_PRICE);
    Assert.True(listing.Price <= MAX_PRICE);
    Assert.True(listing.Bedrooms >= MIN_BEDROOMS);
    Assert.True(listing.Bedrooms <= MAX_BEDROOMS);
    Assert.True(listing.AvailableDate >= DateTime.Now.AddDays(0));
    Assert.True(listing.AvailableDate <= DateTime.Now.AddDays(365));
    Assert.True(listing.HousingType == HousingType.Apt || listing.HousingType == HousingType.House);
  }

  [Fact]
  public void GetAddress_ReturnsValidAddress()
  {
    // Act
    Address address = _fakerFactory.GetAddress();

    // Assert
    Assert.NotNull(address);
    Assert.False(string.IsNullOrEmpty(address.Id));
    Assert.False(string.IsNullOrEmpty(address.StreetAddress1));
    Assert.False(string.IsNullOrEmpty(address.City));
    Assert.False(string.IsNullOrEmpty(address.State));
    Assert.False(string.IsNullOrEmpty(address.Zip));
    Assert.False(string.IsNullOrEmpty(address.County));
  }

  [Fact]
  public void GetPerson_ReturnsValidPerson()
  {
    // Act
    Person person = _fakerFactory.GetPerson();

    // Assert
    Assert.NotNull(person);
    Assert.False(string.IsNullOrEmpty(person.FirstName));
    Assert.False(string.IsNullOrEmpty(person.LastName));
    Assert.False(string.IsNullOrEmpty(person.Email));
    Assert.False(string.IsNullOrEmpty(person.Phone));
    Assert.False(string.IsNullOrEmpty(person.AccountId));
    Assert.IsType<Address>(person.AddressId);
  }

  [Fact]
  public void GetUser_ReturnsValidUser()
  {
    // Act
    User user = _fakerFactory.GetUser();

    // Assert
    Assert.NotNull(user);
    Assert.False(string.IsNullOrEmpty(user.FirstName));
    Assert.False(string.IsNullOrEmpty(user.LastName));
    Assert.False(string.IsNullOrEmpty(user.Email));
    Assert.IsType<List<Listing>>(user.Listings);
  }
}