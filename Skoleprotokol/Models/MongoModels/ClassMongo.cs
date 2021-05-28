using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Skoleprotokol.Models.MongoModels
{
    public class ClassMongo
    {
        [BsonIgnoreIfDefault] // This will allow updating without reentering the ID of the object
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("start")]
        public DateTime Start { get; set; }
        [BsonElement("end")]
        public DateTime End { get; set; }
        [BsonElement("number_of_class")]
        public int NumberOfClass { get; set; }
        [BsonElement("course_id")]
        public string CourseId { get; set; }

        [BsonElement("users")]
        public IEnumerable<UserClass> Users { get; set; }

        public class UserClass
        {
            [BsonIgnoreIfDefault] // This will allow updating without reentering the ID of the object
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            [BsonElement("user")]
            public UserMongo User { get; set; }
            [BsonElement("attendance_key")]
            public string AttendanceKey { get; set; }
            [BsonElement("valid_until")]
            public DateTime ValidUntil { get; set; }
            [BsonElement("present")]
            public bool Present { get; set; }
        }
    }
}
