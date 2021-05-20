using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IAttendanceKeyService<TAttendanceKeyDto, TAttendanceKey>
    {
        Task<string> Generate(AttendanceKeyDto attendanceKeyDto);
        Task<List<AttendanceKeyDto>> GenerateList(int classId);
        Task<bool> IsValid(string attendanceKey);
    }
}
