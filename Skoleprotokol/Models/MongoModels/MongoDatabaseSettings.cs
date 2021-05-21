using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Models.mongo_models
{
    //  The preceding MongoDatabaseSettings class is used to store the 
    //  appsettings.json file's SchoolProtocolMongoDatabaseSettings property values.
    //  The JSON and C# property names are named identically to ease the mapping process.

    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string SchoolCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string SchoolCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}