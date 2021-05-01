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
    public class LessonService : ILessonService<Int32, Int32>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public LessonService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<bool> MakePresent(int classId, int userId) 
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var lessonEntites = await context.Lessons
                    .Where(l => l.ClassIdclass == classId && l.UserIduser == userId)
                    .ToListAsync();

                if(lessonEntites.Any() && lessonEntites != null)
                {
                    foreach (var lesson in lessonEntites)
                    {
                        lesson.Present = true;
                    }

                    await context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
        }
    }
}
