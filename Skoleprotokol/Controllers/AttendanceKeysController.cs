using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.Dtos;
using Skoleprotokol.Repository;
using AutoMapper;
using Skoleprotokol.Services;
using System.Collections.Generic;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class AttendanceKeysController : ControllerBase
    {
        //TODO: Create same service pattern and DTO for attendance keys

        private readonly IMapper _mapper;
        private readonly IUserService<UserDto, NewUserDto> _userService;

        public AttendanceKeysController(IUserService<UserDto, NewUserDto> userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [Route("attendancekey/{classId}")]
        public async Task<IActionResult> GenerateKey(int classId)
        {

            Random random = new Random();

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var generatedId = Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToList();

            if (classId == 0)
            {
                return BadRequest();
            }

            //TODO: create service for attendance keys, and create a new attendance key in db for given class 
            if (true)
            {
                return Created("Attendance key generated", generatedId);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("attendancekey/{classId}")]
        public async Task<IActionResult> CheckAttendanceKey(string attendanceKey, int classId, int userId)
        {
            if (String.IsNullOrEmpty(attendanceKey))
            {
                return BadRequest();
            }
            if (classId == 0)
            {
                return BadRequest();
            }
            if (userId == 0)
            {
                return BadRequest();
            }

            //TODO: get the stored procedure from database and check if attendance key is valid 
            //TODO: if attendance key is valid returns true, set the present to true in the lesson table for given userId and classId
            if (true)
            {
                return Ok();
            }

            return BadRequest();
        }


    }
}