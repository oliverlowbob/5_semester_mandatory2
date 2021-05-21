using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Skoleprotokol.Models.mongo_models;

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
    }
}
