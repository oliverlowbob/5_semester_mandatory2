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
        private MyDBContext myDbContext;

        public UserController(MyDBContext context)
        {
            myDbContext = context;
        }
        
        [HttpGet]
        [Route("users/get_all")]
        public IList<User> Get()
        {
            return (this.myDbContext.Users.ToList());
        }
    }
}
