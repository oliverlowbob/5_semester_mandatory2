using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface ILessonService<TAttendanceKey>
    {
        Task<bool> MakePresent(string attendanceKey);
    }
}
