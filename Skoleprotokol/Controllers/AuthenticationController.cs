using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    /// <summary>
    /// This class handles user authentication
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService<UserLoginDto> _authenticationService;
        private readonly IJwtService<UserDto> _jwtService;
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public AuthenticationController(IAuthenticationService<UserLoginDto> authenticationService,
                IJwtService<UserDto> jwtService,
                IUserService<UserDto, NewUserDto> userService)
        {
            _authenticationService = authenticationService;
            _jwtService = jwtService;
            _userService = userService;
        }

        /// <summary>
        /// Tries to authenticate the user with given user credentials
        /// </summary>
        /// <param name="userCredentials">The user credentials containing email and password</param>
        /// <returns>
        /// <para>A successful authentication returns a 200 (Ok) Status code with an access token for future requests.</para>
        /// <para>A unsuccessful authentication returns a 422 (Unprocessable Entity) Status code.</para>
        /// <para>A wrong request body returns a 400 (Bad Request) Status code.</para>
        /// </returns>
        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginDto userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByEmailAsync(userCredentials.Email);

            if (await _authenticationService.AuthenticateUserAsync(userCredentials, user.Password))
            {
                var accessToken = _jwtService.GenerateAccessToken(user);
                return Ok(new { access_token = accessToken });
            }

            return UnprocessableEntity();
        }
    }
}
