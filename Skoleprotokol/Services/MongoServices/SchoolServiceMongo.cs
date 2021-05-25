using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Skoleprotokol.Models.MongoModels;

namespace Skoleprotokol.Services.MongoServices
{
    public class SchoolServiceMongo
    {
        private readonly IMongoCollection<SchoolMongo> _schoolmongo;

        public SchoolServiceMongo(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _schoolmongo = database.GetCollection<SchoolMongo>(settings.SchoolCollectionName);
        }

        public List<SchoolMongo> Get() =>
            _schoolmongo.Find(SchoolMongo => true).ToList();

        public SchoolMongo Get(string id) =>
            _schoolmongo.Find<SchoolMongo>(schoolmongo => schoolmongo.Id == id).FirstOrDefault();

        /// <summary>
        /// Create a new school
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        public SchoolMongo Create(SchoolMongo school)
        {
            _schoolmongo.InsertOne(school);
            return school;
        }

        public void Update(string id, SchoolMongo schoolIn) =>
           _schoolmongo.ReplaceOne(school => school.Id == id, schoolIn);

        public void Remove(SchoolMongo schoolIn) =>
           _schoolmongo.DeleteOne(school => school.Id == schoolIn.Id);

        /// <summary>
        /// REMOVE V2
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id) =>
            _schoolmongo.DeleteOne(school => school.Id == id);

    }
}
