using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string Name { get; set; }
    }
}
