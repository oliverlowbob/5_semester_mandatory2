using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface ILessonService<TAttendanceKey>
    {
        Task<bool> MakePresent(string attendanceKey);
        Task<List<int>> GetClassIdsByUserId(int userId);
    }
}
