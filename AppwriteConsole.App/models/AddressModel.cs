namespace Models;

public class Address
{
  public string Id { get; set; }
  public string StreetAddress1 { get; set; }
  public string? StreetAddress2 { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Zip { get; set; }
  public string County { get; set; }
}