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

            return Created("Class created", classMongo);
        }

        /// <summary>
        /// Get all classes
        /// </summary>
        /// <returns>All classes</returns>
        [HttpGet]
        [Route("mongo/class/all")]
        public ActionResult<List<ClassMongo>> Get() =>
            _classServiceMongo.Get();

        /// <summary>
        /// Get a class by id
        /// </summary>
        /// <param id="id"></param>
        /// <returns>A class by id</returns>
        [HttpGet]
        [Route("mongo/class/{id}")]
        public ActionResult<ClassMongo> Get(string id)
        {
            var classMongo = _classServiceMongo.Get(id);

            if (classMongo == null)
            {
                return NotFound();
            }

            return classMongo;
        }

      /// <summary>
      /// Delete a class by id
      /// </summary>
      /// <param name="id"></param>
      /// <returns>NoContent</returns>
        [HttpDelete]
        [Route("mongo/class/{id}")]
        public IActionResult Delete(string id)
        {
            var classMongo = _classServiceMongo.Get(id);

            if (classMongo == null)
            {
                return NotFound();
            }

            _classServiceMongo.Remove(classMongo.Id);

            return NoContent();
        }

        /// <summary>
        /// Update a class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="classIn"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("mongo/class/{id}")]
        public IActionResult Update(string id, ClassMongo classIn)
        {
            var classMongo = _classServiceMongo.Get(id);

            if (classMongo == null)
            {
                return NotFound();
            }

            _classServiceMongo.Update(id, classIn);

            return NoContent();
        }


    }
}
