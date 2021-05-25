using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IUserService<TUser, TNewUser>
    {
        Task<TUser> UpdateUserByIdAsync(int userId, TUser user);
        Task<IEnumerable<TUser>> GetAllUsersAsync();
        Task<TUser> GetUserByIdAsync(int userId);
        Task<TUser> GetUserByEmailAsync(string username);
        Task<bool> CreateNewUser(TNewUser user);
    }
}
