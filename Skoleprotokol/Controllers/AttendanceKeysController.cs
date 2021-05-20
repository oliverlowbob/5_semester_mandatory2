using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using AutoMapper;
using Skoleprotokol.Services;
using System.Linq;

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

        [HttpGet]
        [Route("attendancekey/generate/{classId}")]
        public async Task<IActionResult> GenerateKeys(int classId)
        {
            if (classId == 0)
            {
                return BadRequest();
            }

            var generatedKeys = await _attendanceKeyService.GenerateList(classId);

            if (generatedKeys != null && generatedKeys.Any())
            {
                return Created("Attendance key generated", generatedKeys);
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