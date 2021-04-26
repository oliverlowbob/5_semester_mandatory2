using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {
        public UserController()
        {
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            using(var context = new Scool_ProtocolContext())
            {
                return context.Users.Where(u => u.Iduser == 1).FirstOrDefault().SchoolIdschoolNavigation.Name;
            }
        }
    }
}
