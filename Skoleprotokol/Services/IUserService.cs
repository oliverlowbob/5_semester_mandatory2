using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IUserService<TUser> where TUser : class
    {
        Task<bool> UpdateUserById(int userId, TUser user);
        Task<TUser> GetUserById(int userId);
        Task<TUser> GetUserByUsername(string username);
    }
}
