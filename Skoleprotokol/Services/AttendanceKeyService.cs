using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class AttendanceKeyService : IAttendanceKeyService<AttendanceKeyDto, string, int>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public AttendanceKeyService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public AttendanceKeyService()
        {
        }

        public string GenerateKey()
        {
            var id = Guid.NewGuid().ToString("N");

            return id.Substring(0, 10);
        }

        public async Task<string> Generate(AttendanceKeyDto attendanceKeyDto) 
        {
            var attendanceKey = GenerateKey();

            using (var context = _contextFactory.CreateDbContext())
            {
                var transaction = await context.Database.BeginTransactionAsync();

                var attendanceKeyEntity = _mapper.Map<AttendanceKey>(attendanceKeyDto);

                attendanceKeyEntity.IdattendanceKey = attendanceKey;

                await context.AttendanceKeys.AddAsync(attendanceKeyEntity);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return attendanceKey;
            }
        }

        public async Task<List<AttendanceKeyDto>> GenerateList(int classId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var list = new List<AttendanceKeyDto>();

                var transaction = await context.Database.BeginTransactionAsync();

                var lessons = await context.Lessons
                    .Where(l => l.ClassIdclass == classId)
                    .ToListAsync();

                foreach(var lesson in lessons)
                {
                    var attendanceKeyId = GenerateKey();

                    var attendanceKey = new AttendanceKey
                    {
                        IdattendanceKey = attendanceKeyId,
                        LessonClassIdclass = lesson.ClassIdclass,
                        LessonUserIduser = lesson.UserIduser,
                    };

                    await context.AttendanceKeys.AddAsync(attendanceKey);

                    var attendanceKeyDto = _mapper.Map<AttendanceKeyDto>(attendanceKey);

                    var user = await context.Users.FindAsync(lesson.UserIduser);

                    attendanceKeyDto.User = new UserDto
                    {
                        Id = user.Iduser,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                    };

                    attendanceKeyDto.Present = lesson.Present;

                    list.Add(attendanceKeyDto);
                }

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return list;
            }
        }

        public async Task<bool> IsValid(string attendanceKey)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                //USE THIS CODE FOR STORED PROCEDURE FROM DATABASE

                //using (MySqlConnection lconn = new MySqlConnection(context.Database.GetDbConnection().ConnectionString))
                //{
                //    lconn.Open();
                //    using (MySqlCommand cmd = new MySqlCommand())
                //    {
                //        cmd.Connection = lconn;
                //        cmd.CommandText = "get_attendance_key_valid"; // The name of the Stored Proc
                //        cmd.CommandType = CommandType.StoredProcedure; // It is a Stored Proc

                //        cmd.Parameters.AddWithValue("@key_id", attendanceKey);

                //        cmd.Parameters.AddWithValue("@isValid", MySqlDbType.Int16);
                //        cmd.Parameters["@isValid"].Direction = ParameterDirection.Output; // from System.Data

                //        await cmd.ExecuteReaderAsync();

                //        Object obj = cmd.Parameters["@isValid"].Value;
                //        var isValid = (Int32)obj;    // more useful datatype

                //        return Convert.ToBoolean(isValid);
                //    }
                //}

                var isValid = await context.AttendanceKeys.FindAsync(attendanceKey);

                if (isValid != null)
                {
                    if (DateTime.Now <= isValid.ValidUntil)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
