using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.ApiModels;
using Skoleprotokol.Data;
using AutoMapper;
using Skoleprotokol.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;


        public UserController(IGenericRepository<User> repository, IMapper mapper, ILogger<UserController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut]
        [Route("users")]
        public async Task UpdateUser([FromBody] UserApiModel args)
        {

                User user = await _repository.Get(args.Id);

               _repository.GetEntityEntry(user).CurrentValues.SetValues(args);





                await _repository.SaveAll();
          
        }

        [HttpPost]
        [Route("users/enable/{userId}")]
        public async Task EnableUser([FromRoute] int userId)
        {
            User user = await _repository.Get(userId);
            user.Active = true;
            _repository.Update(user);

            await _repository.SaveAll();
        }

        [HttpPost]
        [Route("users/disable/{userId}")]
        public async Task DisableUser([FromRoute] int userId)
        {
            User user = await _repository.Get(userId);
            user.Active = false;
            _repository.Update(user);

          await  _repository.SaveAll();
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            User user = await _repository.Get(userId);
            UserApiModel userToReturn = _mapper.Map<UserApiModel>(user);
            return Ok(userToReturn);
        }
    }
}
