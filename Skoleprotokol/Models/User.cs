using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set; }
        public virtual School School { get; set; }
        public virtual IEnumerable<Role> Roles { get; set; }
        public virtual IEnumerable<Lesson> Lessons { get; set; }
        public virtual IEnumerable<Class> Classes { get; set; }
    }
}
