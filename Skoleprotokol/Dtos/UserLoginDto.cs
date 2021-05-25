using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Dtos
{
    /// <summary>
    /// DTO class for authenticating a user login.
    /// </summary>
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
