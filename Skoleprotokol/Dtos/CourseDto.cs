using System.Collections.Generic;

namespace Skoleprotokol.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClassDto> Classes { get; set; }
    }
}
