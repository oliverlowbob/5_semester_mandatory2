using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{

    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService<UserLoginDto> _authenticationService;
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public AuthenticationController(IAuthenticationService<UserLoginDto> authenticationService,
                IUserService<UserDto, NewUserDto> userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginDto userAuthentication)
        {
            var user = await _userService.GetUserByEmailAsync(userAuthentication.Email);

            if (await _authenticationService.AuthenticateUserAsync(userAuthentication, user.Password))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
