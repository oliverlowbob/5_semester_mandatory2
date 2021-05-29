using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class ClassService : IClassService<ClassDto>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public ClassService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<ClassDto>> GetClassesByIds(List<int> classIds)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var classDtos = new List<ClassDto>();
                var classEntities = await context.Classes.Where(c => classIds.Contains(c.Idclass)).ToListAsync();

                foreach (var classEnt in classEntities)
                {
                    var classDto = _mapper.Map<ClassDto>(classEnt);
                    classDtos.Add(classDto);
                }

                return classDtos;
            }
        }
    }
}
