using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Skoleprotokol.Data;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {
        public UserController()
        {
        }

        [HttpGet]
        [Route("test")]
        public async Task<ApiModels.User> Test()
        {
            using(var context = new Scool_ProtocolContext())
            {
                var users = await context.Users.ToListAsync().ConfigureAwait(false);
                var user = users.ElementAt(1);

                var result = new ApiModels.User
                {
                    Id = user.Iduser,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    SchoolId = user.SchoolIdschool,
                    School = new ApiModels.School
                    {
                        Id = user.SchoolIdschoolNavigation.Idschool,
                        Name = user.SchoolIdschoolNavigation.Name
                    },
                    Lessons = user.Lessons.Select(l => new ApiModels.Lesson
                    {
                        Class = new ApiModels.Class
                        {
                            End = l.ClassIdclassNavigation.End,
                            Start = l.ClassIdclassNavigation.Start,
                            Id = l.ClassIdclass,
                            NumberOfClass = l.ClassIdclassNavigation.NumberOfClass,
                            Course = new ApiModels.Course
                            {
                                Id = l.ClassIdclassNavigation.CourseIdcourse,
                                Name = l.ClassIdclassNavigation.CourseIdcourseNavigation.Name
                            }
                        },
                        Present = l.Present

                    }).ToList(),
                    Roles = user.UserRoles.Select(ur => new ApiModels.Role
                    {
                        Id = ur.RoleIdrole,
                        Name = ur.RoleIdroleNavigation.Role1
                    }).ToList()


                };

                return result;
            }
        }
    }
}
