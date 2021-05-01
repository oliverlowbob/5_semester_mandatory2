using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
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
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (await _userService.CreateNewUser(newUser))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateUserByIdAsync(id, user);

            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return BadRequest();
        }

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