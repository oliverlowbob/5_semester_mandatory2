﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Dtos
{
    public class ClassDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NumberOfClass { get; set; }
        [IgnoreMap]
        public CourseDto Course { get; set; }

    }
}
