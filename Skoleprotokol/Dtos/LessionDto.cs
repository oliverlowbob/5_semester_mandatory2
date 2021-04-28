using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Dtos
{
    public class LessionDto
    {
        public bool Present { get; set; }
       [IgnoreMap]
        public UserDto User { get; set; } 
        public ClassDto Class { get; set; }
    }
}
