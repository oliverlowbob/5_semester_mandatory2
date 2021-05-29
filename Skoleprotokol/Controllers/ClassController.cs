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
        private readonly IdentityController _identityController;

        public ClassController(
            IClassService<ClassDto> classService, 
            ILessonService<string> lessonService, 
            IMapper mapper,
            IdentityController identityController
        )
        {
            _mapper = mapper;
            _classService = classService;
            _lessonService = lessonService;
            _identityController = identityController;
        }

        
        [Authorize]
        [HttpGet]
        [Route("classes")]
        public async Task<List<ClassDto>> GetClasses()
        {
            var identity = User.Identity as ClaimsIdentity;

            var userId = _identityController.GetUserId(identity);

            var classIds = await _lessonService.GetClassIdsByUserId(userId);

            return await _classService.GetClassesByIds(classIds);
        }
    }


}
