using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Dtos;
using Skoleprotokol.Services;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    public class ClassController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClassService<ClassDto> _classService;

        public ClassController(IClassService<ClassDto> classService, IMapper mapper)
        {
            _mapper = mapper;
            _classService = classService;
        }


        
        [Authorize]
        [HttpGet]
        [Route("classes")]
        public async Task<ClassDto> GetClasses()
        {
            return await _classService
        }
    }


}
