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
    public class ClassController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClassService<ClassDto> _classService;
        private readonly ILessonService<string> _lessonService;

        public ClassController(IClassService<ClassDto> classService, ILessonService<string> lessonService, IMapper mapper)
        {
            _mapper = mapper;
            _classService = classService;
            _lessonService = lessonService;
        }

        
        [Authorize]
        [HttpGet]
        [Route("classes")]
        public async Task<List<ClassDto>> GetClasses()
        {
            ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;

            int userId;

            Int32.TryParse(claimsIdentity.Claims.FirstOrDefault().ToString().Split().Last(), out userId);

            var classIds = await _lessonService.GetClassIdsByUserId(userId);

            return await _classService.GetClassesByIds(classIds);
        }
    }


}
