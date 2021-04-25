using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Models
{
    public class Course
    {
        [Key]
        public int IdCourse { get; set; }
        public string Name { get; set; }
    }
}
