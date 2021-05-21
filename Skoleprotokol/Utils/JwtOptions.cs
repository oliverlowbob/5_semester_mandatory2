using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Utils
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int ExpiresInMilliseconds { get; set; }
    }
}
