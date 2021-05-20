using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using Skoleprotokol.Models;
using System;
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

                if(lessonEntites.Any() && lessonEntites != null)
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
    }
}
