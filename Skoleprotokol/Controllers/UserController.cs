using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Skoleprotokol.Data;
using Skoleprotokol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Skoleprotokol.ApiModels;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {
        public UserController()
        {
        }

        //[HttpPut]
        //[Route("users")]
        //public async Task<UserApi> UpdateUser([FromBody] )
        //{

        //}

        [HttpDelete]
        [Route("users/{userId}")]
        public async Task DisableUser(int userId)
        {
            using (var context = new Scool_ProtocolContext())
            {
                var user = await context.Users.FindAsync(userId)
                    .ConfigureAwait(false);

                context.Users.Remove(user);

                await context.SaveChangesAsync()
                    .ConfigureAwait(false);
            }
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<UserApi> GetUser(int userId)
        {
            using (var context = new Scool_ProtocolContext())
            {
                var user = await context.Users.FindAsync(userId)
                    .ConfigureAwait(false);

                var result = new UserApi
                {
                    Id = user.Iduser,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Active = user.Active,
                    SchoolId = user.SchoolIdschool,
                    School = new SchoolApi
                    {
                        Id = user.SchoolIdschoolNavigation.Idschool,
                        Name = user.SchoolIdschoolNavigation.Name
                    },
                    Lessons = user.Lessons.Select(l => new LessonApi
                    {
                        Class = new ClassApi
                        {
                            End = l.ClassIdclassNavigation.End,
                            Start = l.ClassIdclassNavigation.Start,
                            Id = l.ClassIdclass,
                            NumberOfClass = l.ClassIdclassNavigation.NumberOfClass,
                            Course = new CourseApi
                            {
                                Id = l.ClassIdclassNavigation.CourseIdcourse,
                                Name = l.ClassIdclassNavigation.CourseIdcourseNavigation.Name
                            }
                        },
                        Present = l.Present

                    }).ToList(),
                    Roles = user.UserRoles.Select(ur => new RoleApi
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
