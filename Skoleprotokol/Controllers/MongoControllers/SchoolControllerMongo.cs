using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Models.MongoModels;
using Skoleprotokol.Services.MongoServices;
using System.Collections.Generic;

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

        /// <summary>
        /// Get all schools
        /// </summary>
        /// <returns>All schools</returns>
        [HttpGet]
        [Route("mongo/school/all")]
        public ActionResult<List<SchoolMongo>> Get() =>
            _schoolServiceMongo.Get();

        /// <summary>
        /// Get a school by id
        /// </summary>
        /// <param id="id"></param>
        /// <returns>A school by id</returns>
        [HttpGet]
        [Route("mongo/school/{id}")]
        public ActionResult<SchoolMongo> Get(string id)
        {
            var school = _schoolServiceMongo.Get(id);

            if (school == null)
            {
                return NotFound();
            }

            return school;
        }

        /// <summary>
        /// Create a new school
        /// </summary>
        /// <param name="school"></param>
        /// <returns>The created school</returns>
        [HttpPost]
        [Route("mongo/school")]
        public ActionResult<SchoolMongo> Create(SchoolMongo school)
        {
            _schoolServiceMongo.Create(school);

            return CreatedAtRoute("mongo/school", new { id = school.Id.ToString() }, school);
        }

        /// <summary>
        /// Update an existing school
        /// </summary>
        /// <param name="id"></param>
        /// <param name="schoolIn"></param>
        /// <returns>NoContent</returns>
        [HttpPut]
        [Route("mongo/school/{id}")]
        public IActionResult Update(string id, SchoolMongo schoolIn)
        {
            var school = _schoolServiceMongo.Get(id);

            if (school == null)
            {
                return NotFound();
            }

            _schoolServiceMongo.Update(id, schoolIn);

            return NoContent();
        }

        /// <summary>
        /// Delete a school by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent</returns>
        [HttpDelete]
        [Route("mongo/school/{id}")]
        public IActionResult Delete(string id)
        {
            var school = _schoolServiceMongo.Get(id);

            if (school == null)
            {
                return NotFound();
            }

            _schoolServiceMongo.Remove(school.Id);

            return NoContent();
        }
    }
}
