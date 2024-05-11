namespace AppwriteClient.DTOs
{
    public class SeedDatabaseDTO
    {
        public string DatabaseName { get; set; }
        public List<CollectionDTO> Collections { get; set; }
    }
}
