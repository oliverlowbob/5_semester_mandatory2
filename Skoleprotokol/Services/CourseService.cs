using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class CourseService : ICourseService<CourseDto>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public CourseService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<CourseDto> GetCourseById(int courseId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var courseEntity = await context.Courses.FirstOrDefaultAsync(c => c.Idcourse == courseId);
                var courseDto = _mapper.Map<CourseDto>(courseEntity);
                return courseDto;
            }

        }

    }
}
