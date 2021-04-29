using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Dtos
{
    /// <summary>
    /// DTO class for when registering a new user.
    /// </summary>
    public class NewUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int SchoolId { get; set; }
    }
}
