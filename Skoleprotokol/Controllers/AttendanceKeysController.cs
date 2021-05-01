using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using Skoleprotokol.Repository;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class AttendanceKeysController : ControllerBase
    {
        //TODO: Create same service pattern and DTO for attendance keys

        private readonly IMapper _mapper;
        private readonly IAttendanceKeyService<AttendanceKeyDto, string> _attendanceKeyService;

        public AttendanceKeysController(IAttendanceKeyService<AttendanceKeyDto, string> attendanceKeyService, IMapper mapper)
        {
            _mapper = mapper;
            _attendanceKeyService = attendanceKeyService;
        }

        [HttpPost]
        [Route("attendancekey")]
        public async Task<IActionResult> GenerateKey(AttendanceKeyDto attendanceKey)
        {
            if (attendanceKey.LessonUserIdclass == 0)
            {
                return BadRequest();
            }
            if (attendanceKey.LessonUserIduser == 0)
            {
                return BadRequest();
            }

            //TODO: create service for attendance keys, and create a new attendance key in db for given class 
            if (await _attendanceKeyService.Generate(attendanceKey))
            {
                return Created("Attendance key generated", "Attendance key generated");
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("attendancekey/{attendanceKey}")]
        public async Task<IActionResult> IsValid(string attendanceKey)
        {
            if (String.IsNullOrEmpty(attendanceKey))
            {
                return BadRequest();
            }

            //TODO: get the stored procedure from database and check if attendance key is valid 
            //TODO: if attendance key is valid returns true, set the present to true in the lesson table for given userId and classId
            if (true)
            {
                return Ok();
            }

            return BadRequest();
        }


    }
}