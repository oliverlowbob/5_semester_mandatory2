using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Skoleprotokol.Models.MongoModels
{
    public class SchoolMongo
    {
        [BsonIgnoreIfDefault] // This will allow updating without reentering the ID of the object
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        public Address[] address { get; set; }

        public class Address
        {

            [BsonElement("street")]
            public string Street { get; set; }
            [BsonElement("postal_code")]
            public int PostalCode { get; set; }
            [BsonElement("country")]
            public string Country { get; set; }
            [BsonElement("number")]
            public int Number { get; set; }
        }



    }

}
