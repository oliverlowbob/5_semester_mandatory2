using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Skoleprotokol.Models.mongo_models
{
    public class SchoolMongo
    {
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
