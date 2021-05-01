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
using System.Text;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class AttendanceKeyService : IAttendanceKeyService<AttendanceKeyDto, string>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public AttendanceKeyService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<string> Generate(AttendanceKeyDto attendanceKeyDto) 
        {
            var id = Guid.NewGuid().ToString("N");

            var attendanceKey = id.Substring(0, 9);

            using (var context = _contextFactory.CreateDbContext())
            {
                var attendanceKeyEntity = _mapper.Map<AttendanceKey>(attendanceKeyDto);

                attendanceKeyEntity.IdattendanceKey = attendanceKey;

                await context.AttendanceKeys.AddAsync(attendanceKeyEntity);

                await context.SaveChangesAsync();

                return attendanceKey;
            }
        }

        public async Task<bool> IsValid(string attendanceKey)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                using (MySqlConnection lconn = new MySqlConnection(context.Database.GetDbConnection().ConnectionString))
                {
                    lconn.Open();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = lconn;
                        cmd.CommandText = "get_attendance_key_valid"; // The name of the Stored Proc
                        cmd.CommandType = CommandType.StoredProcedure; // It is a Stored Proc

                        cmd.Parameters.AddWithValue("@key_id", attendanceKey);

                        cmd.Parameters.AddWithValue("@isValid", MySqlDbType.Int16);
                        cmd.Parameters["@isValid"].Direction = ParameterDirection.Output; // from System.Data

                        await cmd.ExecuteReaderAsync();

                        Object obj = cmd.Parameters["@isValid"].Value;
                        var isValid = (Int32)obj;    // more useful datatype

                        return Convert.ToBoolean(isValid);
                    }
                }
            }
        }
    }
}
