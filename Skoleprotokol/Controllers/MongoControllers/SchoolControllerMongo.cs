using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Models.mongo_models;
using Skoleprotokol.Services.MongoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers.MongoControllers
{
    [ApiController]
    public class SchoolControllerMongo : ControllerBase
    {
        private readonly SchoolServiceMongo _schoolServiceMongo;

        public SchoolControllerMongo(SchoolServiceMongo schoolServiceMongo)
        {
            _schoolServiceMongo = schoolServiceMongo;
        }

        [HttpGet]
        [Route("mongo/school/all")]
        public ActionResult<List<SchoolMongo>> Get() =>
            _schoolServiceMongo.Get();
    }
}
