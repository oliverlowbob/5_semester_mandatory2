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
        private readonly IAttendanceKeyService<AttendanceKeyDto, string, int> _attendanceKeyService;
        private readonly ILessonService<string> _lessonService;

        public AttendanceKeysController(
            IAttendanceKeyService<AttendanceKeyDto, string, int> attendanceKeyService, 
            IMapper mapper, 
            ILessonService<string> lessonService
        )
        {
            _mapper = mapper;
            _attendanceKeyService = attendanceKeyService;
            _lessonService = lessonService;
        }

        /// <summary>
        /// Generate key on given userId and classId
        /// </summary>
        /// <param name="attendanceKey"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates keys for all students in given class
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if given attendance key is valid (10 minute timeframe after creation) 
        /// </summary>
        /// <param name="attendanceKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attendancekey/validate/{attendanceKey}")]
        public async Task<IActionResult> Validate(string attendanceKey)
        {
            if (String.IsNullOrEmpty(attendanceKey))
            {
                return BadRequest();
            }

            var isValid = await _attendanceKeyService.IsValid(attendanceKey);

            if (isValid)
            {
                var isSuccess = await _lessonService.MakePresent(attendanceKey);

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