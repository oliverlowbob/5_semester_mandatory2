using Skoleprotokol.Dtos;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface ILessonService<TClass, TUser>
    {
        Task<bool> MakePresent(int classId, int userId);
    }
}
