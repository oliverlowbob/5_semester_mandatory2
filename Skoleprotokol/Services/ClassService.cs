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

        public async Task<ClassDto> GetClassesByIds(List<int> classIds)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var classEntities = await context.Classes.Where(c => classIds.Contains(c.Idclass)).ToListAsync();
                var classDto = _mapper.Map<ClassDto>(classEntities);

                return classDto;
            }
        }
    }
}
