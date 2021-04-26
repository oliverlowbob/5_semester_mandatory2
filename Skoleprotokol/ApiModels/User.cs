using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class User
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public School School { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
