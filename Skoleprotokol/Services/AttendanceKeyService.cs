using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Random random = new Random();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var generatedId = Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToList();

            using (var context = _contextFactory.CreateDbContext())
            {
                var attendanceKeyEntity = _mapper.Map<AttendanceKey>(attendanceKeyDto);

                attendanceKeyEntity.IdattendanceKey = generatedId.ToString();

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
