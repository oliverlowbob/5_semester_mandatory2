using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Skoleprotokol.Models.MongoModels;

namespace Skoleprotokol.Services.MongoServices
{
    public class ClassServiceMongo
    {
        private readonly IMongoCollection<ClassMongo> _classmongo;

        public ClassServiceMongo(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _classmongo = database.GetCollection<ClassMongo>(settings.ClassCollectionName);
        }

        /// <summary>
        /// Create a new school
        /// </summary>
        /// <param name="classMongo"></param>
        /// <returns></returns>
        public ClassMongo Create(ClassMongo classMongo)
        {
            _classmongo.InsertOne(classMongo);
            return classMongo;
        }

        public List<ClassMongo> Get() =>
            _classmongo.Find(ClassMongo => true).ToList();

    }
}
