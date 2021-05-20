using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ClassDto> Classes { get; set; }
    }
}
