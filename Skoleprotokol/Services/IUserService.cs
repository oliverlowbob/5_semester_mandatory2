using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IUserService<TUser> where TUser : class
    {
        Task<bool> UpdateUserByIdAsync(int userId, TUser user);
        Task<IEnumerable<TUser>> GetAllUsersAsync();
        Task<TUser> GetUserByIdAsync(int userId);
        Task<TUser> GetUserByEmailAsync(string username);
    }
}
