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
    public string AvailableDate { get; set; }
    public string HousingType { get; set; }
  }
}