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
        private readonly UserActionController _identityController;

        public ClassController(
            IClassService<ClassDto> classService, 
            ILessonService<string> lessonService, 
            IMapper mapper,
            UserActionController identityController
        )
        {
            _mapper = mapper;
            _classService = classService;
            _lessonService = lessonService;
            _identityController = identityController;
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

            var userId = _identityController.GetUserId(identity);

            var isTeacher = await _identityController.IsTeacher(userId);

            if (!isTeacher)
            {
                return Unauthorized($"User with id {userId} is not teacher");
            }

            var classIds = await _lessonService.GetClassIdsByUserId(userId);

            return Ok(await _classService.GetClassesByIds(classIds));
        }
    }


}
