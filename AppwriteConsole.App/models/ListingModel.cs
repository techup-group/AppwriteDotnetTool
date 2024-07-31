namespace Models
{
  public class Listing
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int Price { get; set; }
    public int Bedrooms { get; set; }
    public bool PetsAllowed { get; set; }
    public bool ParkingAvailable { get; set; }
    public DateTime AvailableDate { get; set; }
    public HousingType HousingType { get; set; }
  }
}

public enum HousingType
{
  Apt,
  House,
}