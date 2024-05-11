using Appwrite.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            public string Type { get;  set; }
            public static RelationshipType ToRelationsipType(string relationshipType) => relationshipType switch
            {
                _ when relationshipType == RelationshipType.OneToOne.Value => RelationshipType.OneToOne,
                _ when relationshipType == RelationshipType.OneToMany.Value => RelationshipType.OneToMany,
                _ when relationshipType == RelationshipType.ManyToMany.Value => RelationshipType.ManyToMany,
                _ => throw new ArgumentNullException(nameof(relationshipType), "Value cannot be null."),
            };
            public bool TwoWay { get; set; }
            public string Key { get; set; }
            public string TwoWayKey { get; set; }
            public string OnDelete { get;  set; }
            public static RelationMutate? ToRelationMutate(string relationMutate) => relationMutate switch
            {
                _ when relationMutate == RelationMutate.Cascade.Value => RelationMutate.Cascade,
                _ when relationMutate == RelationMutate.Restrict.Value => RelationMutate.Restrict,
                _ when relationMutate == RelationMutate.SetNull.Value => RelationMutate.SetNull,
                _ => null,
            };
        }
    }
}
