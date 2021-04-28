using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.ApiModels;
using Skoleprotokol.Repository;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {

        UserRepository userRepository = new UserRepository();

        public UserController()
        {
        }

        [HttpPut]
        [Route("users")]
        public async Task UpdateUser([FromBody] UserApiModel args)
        {
            await userRepository.UpdateUser(args);
        }

        [HttpPost]
        [Route("users/enable/{userId}")]
        public async Task EnableUser(int userId)
        {
            await userRepository.EnableUser(userId);
        }

        [HttpPost]
        [Route("users/disable/{userId}")]
        public async Task DisableUser(int userId)
        {
            await userRepository.DisableUser(userId);
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<UserApiModel> GetUser(int userId)
        {
            return await userRepository.GetUser(userId);
        }
    }
}