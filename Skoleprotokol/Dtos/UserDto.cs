using System.Collections.Generic;

namespace Skoleprotokol.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }

        public SchoolDto School { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public IEnumerable<LessonDto> Lessons { get; set; }
    }
}
