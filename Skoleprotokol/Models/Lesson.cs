using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Models
{
    public class Lesson
    {
        public Boolean Present { get; set; }
        public Class Class { get; set; }
        public User User { get; set; }

    }
}
