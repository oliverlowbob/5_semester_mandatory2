using AutoMapper;

namespace Skoleprotokol.Dtos
{
    public class LessonDto
    {
        public bool Present { get; set; }
        [IgnoreMap]
        public UserDto User { get; set; } 
        public ClassDto Class { get; set; }
    }
}
