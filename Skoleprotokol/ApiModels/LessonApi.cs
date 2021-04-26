using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class LessonApi
    {
        public bool Present { get; set; }
        public UserApi User { get; set; }
        public ClassApi Class { get; set; }
    }
}
