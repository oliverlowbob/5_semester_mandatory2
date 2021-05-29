using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;
using System.Security.Claims;

namespace Skoleprotokol.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService<UserDto, NewUserDto> _userService;
        private readonly IdentityController _identityController;

        public UserController(IUserService<UserDto, NewUserDto> userService, IdentityController identityController, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _identityController = identityController;
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> CreateUser([FromBody] NewUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = User.Identity as ClaimsIdentity;

            var userId = _identityController.GetUserId(identity);

            var isAdmin = await _identityController.IsAdmin(userId);

            if (!isAdmin)
            {
                return Unauthorized($"User with id {userId} is not admin");
            }
            
            if (await _userService.CreateNewUser(newUser))
            {
                return Created("created", newUser);
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates a user from given id and user body
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = User.Identity as ClaimsIdentity;

            var userId = _identityController.GetUserId(identity);

            var isAdmin = await _identityController.IsAdmin(userId);

            if (!isAdmin)
            {
                return Unauthorized($"User with id {userId} is not admin");
            }

            var updatedUser = await _userService.UpdateUserByIdAsync(id, user);

            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users")]
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        /// <summary>
        /// Get specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("users/{id}")]
        public async Task<UserDto> GetUser(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }
    }
}