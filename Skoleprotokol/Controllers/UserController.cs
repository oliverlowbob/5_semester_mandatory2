using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public UserController(IUserService<UserDto, NewUserDto> userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
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
            //TODO: Use the code beneath to get userid and validate user role is admin
            //var test = tokenHandler.ReadJwtToken(token);
            //var userId = test.Claims.Where(c => c.Value == "UserId").FirstOrDefault().Value;
            //Pseudo code
            //Get all roles from userId
            //Check if one of the roles is ADMIN
            //if not return Unauthorized();

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