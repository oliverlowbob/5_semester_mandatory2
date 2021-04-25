using System.ComponentModel.DataAnnotations;

namespace Skoleprotokol.Models
{
    public class School
    {
        [Key]
        public int IdSchool { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }
        public int HouseNumber { get; set; }
    }
}
