using Appwrite.Enums;

namespace AppwriteClient.DTOs
{
    public class AttributeDTO
    {
        public class StringAttribute
        {
            public string Key { get; set; }
            public int Size { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
            public bool Encrypt { get; set; }
        }

        public class EmailAttribute
        {
            public string Key { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
        }

        public class EnumAttribute
        {
            public string Key { get; set; }
            public List<string> Elements { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
        }

        public class IPAddressAttribute
        {
            public string Key { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
        }

        public class URLAttribute
        {
            public string Key { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
        }

        public class IntegerAttribute
        {
          
            public string Key { get; set; }
            public bool Required { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public int? Default { get; set; }
            public bool Array { get; set; }
        }

        public class FloatAttribute
        {
           
            public string Key { get; set; }
            public bool Required { get; set; }
            public float Min { get; set; }
            public float Max { get; set; }
            public float Default { get; set; }
            public bool Array { get; set; }
        }

        public class BooleanAttribute
        {
           
            public string Key { get; set; }
            public bool Required { get; set; }
            public bool Default { get; set; }
            public bool Array { get; set; }
        }

        public class DatetimeAttribute
        {
           
            public string Key { get; set; }
            public bool Required { get; set; }
            public string Default { get; set; }
            public bool Array { get; set; }
        }

        public class RelationshipAttribute
        {
           
            public string RelatedCollectionId { get; set; }
            public RelationshipType Type { get; set; }
            public bool TwoWay { get; set; }
            public string Key { get; set; }
            public string TwoWayKey { get; set; }
            public RelationMutate? OnDelete { get; set; }
        }
    }
}
