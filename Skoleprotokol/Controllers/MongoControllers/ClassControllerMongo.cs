using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Models.MongoModels;
using Skoleprotokol.Services.MongoServices;
using System;
using System.Collections.Generic;

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
        /// Create a class
        /// </summary>
        /// <param name="classMongo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mongo/class/")]
        public ActionResult<ClassMongo> Create(ClassMongo classMongo)
        {
            var newUsers = new List<ClassMongo.UserClass>();

            foreach (var user in classMongo.Users)
            {
                var id = Guid.NewGuid().ToString("N");

                var attendanceKey = id.Substring(0, 9);

                user.AttendanceKey = attendanceKey;

                user.ValidUntil = DateTime.Now.AddMinutes(10);

                newUsers.Add(user);
            }

            classMongo.Users = newUsers;

            _classServiceMongo.Create(classMongo);

            return Created("User created", classMongo);
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
