using AutoMapper.Configuration.Annotations;
using System;

namespace Skoleprotokol.Dtos
{
    public partial class AttendanceKeyDto
    {
        public int LessonUserIduser { get; set; }
        public int LessonUserIdclass { get; set; }
        public string Value { get; set; }

    }
}