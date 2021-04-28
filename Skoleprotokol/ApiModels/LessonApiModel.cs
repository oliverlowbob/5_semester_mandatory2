using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.ApiModels
{
    public class LessonApiModel
    {
        public bool Present { get; set; }
        public UserApiModel User { get; set; }
        public ClassApiModel Class { get; set; }
    }
}
