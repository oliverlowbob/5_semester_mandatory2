using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class LessonService : ILessonService<string>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public LessonService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<bool> MakePresent(string attendanceKey)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var transaction = await context.Database.BeginTransactionAsync();

                var lessonEntites = await context.Lessons
                    .Where(l => l.AttendanceKeys.Any(a => a.IdattendanceKey == attendanceKey))
                    .ToListAsync();

                if (lessonEntites != null && lessonEntites.Any())
                {
                    foreach (var lesson in lessonEntites)
                    {
                        lesson.Present = true;
                    }

                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                await transaction.CommitAsync();

                return false;
            }
        }

        public async Task<List<int>> GetClassIdsByUserId(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var transaction = await context.Database.BeginTransactionAsync();

                var lessons = await context.Lessons
                    .Where(l => l.UserIduser == userId)
                    .Select(l => l.ClassIdclass)
                    .ToListAsync();
                
                await transaction.CommitAsync();

                return lessons;
            }
        }
    }
}
