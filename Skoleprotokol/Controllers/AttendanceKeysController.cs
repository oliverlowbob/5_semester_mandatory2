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
    public class AttendanceKeysController : ControllerBase
    {
        private readonly Scool_ProtocolContext _context;

        public AttendanceKeysController(Scool_ProtocolContext context)
        {
            _context = context;
        }

        // GET: api/AttendanceKeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceKey>>> GetAttendanceKeys()
        {
            return await _context.AttendanceKeys.ToListAsync();
        }

        // GET: api/AttendanceKeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceKey>> GetAttendanceKey(string id)
        {
            var attendanceKey = await _context.AttendanceKeys.FindAsync(id);

            if (attendanceKey == null)
            {
                return NotFound();
            }

            return attendanceKey;
        }

        // PUT: api/AttendanceKeys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendanceKey(string id, AttendanceKey attendanceKey)
        {
            if (id != attendanceKey.IdattendanceKey)
            {
                return BadRequest();
            }

            _context.Entry(attendanceKey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceKeyExists(id))
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

        // POST: api/AttendanceKeys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AttendanceKey>> PostAttendanceKey(AttendanceKey attendanceKey)
        {
            _context.AttendanceKeys.Add(attendanceKey);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AttendanceKeyExists(attendanceKey.IdattendanceKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAttendanceKey", new { id = attendanceKey.IdattendanceKey }, attendanceKey);
        }

        // DELETE: api/AttendanceKeys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttendanceKey>> DeleteAttendanceKey(string id)
        {
            var attendanceKey = await _context.AttendanceKeys.FindAsync(id);
            if (attendanceKey == null)
            {
                return NotFound();
            }

            _context.AttendanceKeys.Remove(attendanceKey);
            await _context.SaveChangesAsync();

            return attendanceKey;
        }

        private bool AttendanceKeyExists(string id)
        {
            return _context.AttendanceKeys.Any(e => e.IdattendanceKey == id);
        }
    }
}
