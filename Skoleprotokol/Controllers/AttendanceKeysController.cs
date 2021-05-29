using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class AttendanceKeysController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceKeyService<AttendanceKeyDto, string, int> _attendanceKeyService;
        private readonly ILessonService<string> _lessonService;
        private readonly UserActionController _userActionController;

        public AttendanceKeysController(
            IAttendanceKeyService<AttendanceKeyDto, string, int> attendanceKeyService,
            IMapper mapper,
            ILessonService<string> lessonService,
            UserActionController userActionController
        )
        {
            _mapper = mapper;
            _attendanceKeyService = attendanceKeyService;
            _lessonService = lessonService;
            _userActionController = userActionController;
        }

        /// <summary>
        /// Generate key on given userId and classId
        /// </summary>
        /// <param name="attendanceKey"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("attendancekey/generate")]
        [Authorize]
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

            var identity = User.Identity as ClaimsIdentity;

            var userId = _userActionController.GetUserId(identity);

            var isTeacher = await _userActionController.IsTeacher(userId);

            if (!isTeacher)
            {
                return Unauthorized($"User with id {userId} is not teacher");
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
        [Authorize]
        public async Task<IActionResult> GenerateKeys(int classId)
        {
            if (classId == 0)
            {
                return BadRequest();
            }

            var identity = User.Identity as ClaimsIdentity;

            var userId = _userActionController.GetUserId(identity);

            var isTeacher = await _userActionController.IsTeacher(userId);

            if (!isTeacher)
            {
                return Unauthorized($"User with id {userId} is not teacher");
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
        [AllowAnonymous]
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