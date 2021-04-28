using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Models;

namespace Skoleprotokol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        public SchoolsController()
        {
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<School>>> GetSchools()
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                return await dbContext.Schools.ToListAsync();
            }

        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<School>> GetSchool(int id)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var school = await dbContext.Schools.FindAsync(id);

                if (school == null)
                {
                    return NotFound();
                }

                return school;
            }
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(int id, School school)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                if (id != school.Idschool)
                {
                    return BadRequest();
                }

                dbContext.Entry(school).State = EntityState.Modified;

                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
        }

        // POST: api/Schools
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(School school)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                dbContext.Schools.Add(school);
                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (SchoolExists(school.Idschool))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetSchool", new { id = school.Idschool }, school);
            }

        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<School>> DeleteSchool(int id)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var school = await dbContext.Schools.FindAsync(id);
                if (school == null)
                {
                    return NotFound();
                }

                dbContext.Schools.Remove(school);
                await dbContext.SaveChangesAsync();

                return school;
            }

        }

        private bool SchoolExists(int id)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                return dbContext.Schools.Any(e => e.Idschool == id);
            }
        }
    }
}
