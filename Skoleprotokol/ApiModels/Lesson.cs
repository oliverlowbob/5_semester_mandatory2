using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class Lesson
    {
        public bool Present { get; set; }
        public User User { get; set; }
        public Class Class { get; set; }
    }
}
