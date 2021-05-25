using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Skoleprotokol.Models.MongoModels
{
    public class UserMongo
    {
        [BsonIgnoreIfDefault] // This will allow updating without reentering the ID of the object
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }
        [BsonElement("last_name")]
        public string LastName { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("active")]
        public bool? Active { get; set; }
        [BsonElement("role")]
        public string Role { get; set; }
        [BsonElement("school_id")]
        public string SchoolId { get; set; }


    }
}
