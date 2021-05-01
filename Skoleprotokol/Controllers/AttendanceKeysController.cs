using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class AttendanceKeysController : ControllerBase
    {
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

            var generatedKey = await _attendanceKeyService.Generate(attendanceKey);

            if (!String.IsNullOrEmpty(generatedKey))
            {
                return Created("Attendance key generated", generatedKey);
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

            var isValid = await _attendanceKeyService.IsValid(attendanceKey);

            return Ok(isValid);
        }


    }
}