using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Models.MongoModels;
using Skoleprotokol.Services.MongoServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers.MongoControllers
{
    [ApiController]
    public class ClassControllerMongo : ControllerBase
    {
        private readonly ClassServiceMongo _classServiceMongo;

        public ClassControllerMongo(ClassServiceMongo classServiceMongo)
        {
            _classServiceMongo = classServiceMongo;
        }

        /// <summary>
        /// Get all classes
        /// </summary>
        /// <returns>All classes</returns>
        [HttpGet]
        [Route("mongo/class/all")]
        public ActionResult<List<ClassMongo>> Get() =>
            _classServiceMongo.Get();


    }
}
