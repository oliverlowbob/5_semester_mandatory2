using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Skoleprotokol.Models.MongoModels
{
    public class CourseMongo
    {
        [BsonIgnoreIfDefault] // This will allow updating without reentering the ID of the object
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

    }
}
