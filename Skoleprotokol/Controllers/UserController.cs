using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using Skoleprotokol.Repository;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public UserController(IUserService<UserDto, NewUserDto> userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("users")]
        public async Task<bool> CreateUser([FromBody] NewUserDto newUser)
        {
            return await _userService.CreateNewUser(newUser);
        }

        [HttpPut]
        [Route("users/{id}")]
        public async Task<bool> UpdateUser(int id, [FromBody] UserDto user)
        {
            return await _userService.UpdateUserByIdAsync(id, user);
        }

        /*
        [HttpPost]
        [Route("users/enable/{userId}")]
        public async Task EnableUser(int userId)
        {
            await _userService.EnableUser(userId);
        }

        [HttpPost]
        [Route("users/disable/{userId}")]
        public async Task DisableUser(int userId)
        {
            await _userService.DisableUser(userId);
        }*/

        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<UserDto>> GetAllUsers(int userId)
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<UserDto> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
    }
}