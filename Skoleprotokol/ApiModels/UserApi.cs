using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class UserApi
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }

        public SchoolApi School { get; set; }
        public IEnumerable<RoleApi> Roles { get; set; }
        public IEnumerable<LessonApi> Lessons { get; set; }
    }
}
