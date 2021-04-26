using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class UserApiModel
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }

        public SchoolApiModel School { get; set; }
        public IEnumerable<RoleApiModel> Roles { get; set; }
        public IEnumerable<LessonApiModel> Lessons { get; set; }
    }
}
