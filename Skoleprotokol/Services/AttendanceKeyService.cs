using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
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

        public async Task<bool> Generate(AttendanceKeyDto attendanceKeyDto) 
        {
            var id = Guid.NewGuid().ToString("N");

            var attendanceKey = id.Substring(0, 9);

            using (var context = _contextFactory.CreateDbContext())
            {
                var attendanceKeyEntity = _mapper.Map<AttendanceKey>(attendanceKeyDto);

                attendanceKeyEntity.IdattendanceKey = attendanceKey;

                await context.AttendanceKeys.AddAsync(attendanceKeyEntity);

                return await context.SaveChangesAsync() == 1;
            }
        }

        public async Task<bool> IsValid(string attendanceKey)
        {
            return true;
        }


    }
}
