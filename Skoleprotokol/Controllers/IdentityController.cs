using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    public class IdentityController
    {
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public IdentityController(IUserService<UserDto, NewUserDto> userService)
        {
            _userService = userService;
        }

        public int GetUserId(ClaimsIdentity user)
        {
            int userId;

            Int32.TryParse(user.Claims.FirstOrDefault().ToString().Split().Last(), out userId);

            return userId;
        }

        public async Task<bool> IsAdmin(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var isAdmin = user.Roles.Where(r => r.Id == 1);

            if (!isAdmin.Any())
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsTeacher(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var isAdmin = user.Roles.Where(r => r.Id == 3);

            if (!isAdmin.Any())
            {
                return false;
            }

            return true;
        }
    }
}
