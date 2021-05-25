using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface ICourseService<TCourse>
    {
        Task<TCourse> GetCourseById(int courseId);
    }
}
