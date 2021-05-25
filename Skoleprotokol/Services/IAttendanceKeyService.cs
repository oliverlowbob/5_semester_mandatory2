using Skoleprotokol.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IAttendanceKeyService<TAttendanceKeyDto, TAttendanceKey, TClassId>
    {
        Task<string> Generate(AttendanceKeyDto attendanceKeyDto);
        Task<List<AttendanceKeyDto>> GenerateList(int classId);
        Task<bool> IsValid(string attendanceKey);
    }
}
