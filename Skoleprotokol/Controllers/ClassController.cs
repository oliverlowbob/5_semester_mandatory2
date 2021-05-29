using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClassService<ClassDto> _classService;
        private readonly ILessonService<string> _lessonService;
        private readonly UserActionController _userActionController;

        public ClassController(
            IClassService<ClassDto> classService, 
            ILessonService<string> lessonService, 
            IMapper mapper,
            UserActionController userActionController
        )
        {
            _mapper = mapper;
            _classService = classService;
            _lessonService = lessonService;
            _userActionController = userActionController;
        }

        
        /// <summary>
        /// Gets all classes for a logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("classes")]
        public async Task<IActionResult> GetClasses()
        {
            var identity = User.Identity as ClaimsIdentity;

            var userId = _userActionController.GetUserId(identity);

            var isTeacher = await _userActionController.IsTeacher(userId);

            if (!isTeacher)
            {
                return Unauthorized($"User with id {userId} is not teacher");
            }

            var classIds = await _lessonService.GetClassIdsByUserId(userId);

            return Ok(await _classService.GetClassesByIds(classIds));
        }
    }


}
