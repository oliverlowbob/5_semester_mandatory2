using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IClassService<TClass>
    {
        Task<List<TClass>> GetClassesByIds(List<int> classIds);
    }
}
