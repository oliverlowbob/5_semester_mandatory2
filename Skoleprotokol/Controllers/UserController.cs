using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skoleprotokol.DbContexts;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _myDbContext;

        public UserController(MyDBContext context)
        {
            _myDbContext = context;
        }
        
        [HttpGet]
        [Route("users/get_all")]
        public IList<User> Get()
        {
            return _myDbContext.User.ToList();
        }
    }
}
