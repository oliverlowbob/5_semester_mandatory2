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
        private readonly ILessonService<Int32, Int32> _lessonService;

        public AttendanceKeysController(
            IAttendanceKeyService<AttendanceKeyDto, string> attendanceKeyService, 
            IMapper mapper, 
            ILessonService<Int32, Int32> lessonService
        )
        {
            _mapper = mapper;
            _attendanceKeyService = attendanceKeyService;
            _lessonService = lessonService;
        }

        [HttpPost]
        [Route("attendancekey/generate")]
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

        [HttpPost]
        [Route("attendancekey/validate")]
        public async Task<IActionResult> IsValid(AttendanceKeyDto attendanceKey)
        {
            if (String.IsNullOrEmpty(attendanceKey.Value))
            {
                return BadRequest();
            }

            var isValid = await _attendanceKeyService.IsValid(attendanceKey.Value);

            if (isValid)
            {
                var isSuccess = await _lessonService.MakePresent(attendanceKey.LessonUserIdclass, attendanceKey.LessonUserIduser);

                if (isSuccess)
                {
                    return Ok(isValid);
                }

                return BadRequest("Wrong userId or classId");
            }

            return BadRequest("Attendance key is outdated/wrong");
        }


    }
}