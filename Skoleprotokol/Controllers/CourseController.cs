using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourseService<CourseDto> _courseService;

        public CourseController(ICourseService<CourseDto> courseService, IMapper mapper)
        {
            _mapper = mapper;
            _courseService = courseService;
        }


        /// <summary>
        /// Gets a specific course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns>The specified course</returns>
        [Authorize]
        [HttpGet]
        [Route("courses/{courseId}")]
        public async Task<CourseDto> GetCourse(int courseId)
        {
            return await _courseService.GetCourseById(courseId);
        }
    }


}
