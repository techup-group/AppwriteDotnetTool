using System.Text.Json.Serialization;
using static AppwriteClient.DTOs.AttributeDTO;

namespace AppwriteClient.DTOs
{
    public class CollectionDTO
    {
        public CollectionDTO()
        {
            StringAttributes = new List<StringAttribute>();
            EmailAttributes = new List<EmailAttribute>();
            EnumAttributes = new List<EnumAttribute>();
            IPAddressAttributes = new List<IPAddressAttribute>();
            URLAttributes = new List<URLAttribute>();
            IntegerAttributes = new List<IntegerAttribute>();
            FloatAttributes = new List<FloatAttribute>();
            BooleanAttributes = new List<BooleanAttribute>();
            DatetimeAttributes = new List<DatetimeAttribute>();
            RelationshipAttributes = new List<RelationshipAttribute>();
        }


        public string CollectionId { get; set; }
        public string Name { get; set; }
        public List<StringAttribute> StringAttributes { get; set; }
        public List<EmailAttribute> EmailAttributes { get; set; }
        public List<EnumAttribute> EnumAttributes { get; set; }
        public List<IPAddressAttribute> IPAddressAttributes { get; set; }
        public List<URLAttribute> URLAttributes { get; set; }
        public List<IntegerAttribute> IntegerAttributes { get; set; }
        public List<FloatAttribute> FloatAttributes { get; set; }
        public List<BooleanAttribute> BooleanAttributes { get; set; }
        public List<DatetimeAttribute> DatetimeAttributes { get; set; }
        public List<RelationshipAttribute> RelationshipAttributes { get; set; }

    }
}
