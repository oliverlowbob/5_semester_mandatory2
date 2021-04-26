using Microsoft.AspNetCore.Mvc;
using Skoleprotokol.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Skoleprotokol.ApiModels;
using Skoleprotokol.Repository;

namespace Skoleprotokol.Controllers
{
    [ApiController]
    public class UserController : ApiController
    {

        public UserController()
        {
        }

        [HttpPut]
        [Route("users")]
        public async Task UpdateUser([FromBody] UserApiModel args)
        {
            var userRepository = new UserRepository();

            if(args == null)
            {
                throw new ArgumentNullException("No args given");
            }

            if(args.Id == 0)
            {
                throw new ArgumentNullException("No user id");
            }

            await userRepository.UpdateUser(args);
        }

        [HttpPost]
        [Route("users/enable/{userId}")]
        public async Task EnableUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                if(user.Active == true)
                {
                    return;
                }

                user.Active = true;

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }


        [HttpPost]
        [Route("users/disable/{userId}")]
        public async Task DisableUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                if(user.Active == false)
                {
                    return;
                }

                user.Active = false;

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<UserApiModel> GetUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                var result = new UserApiModel
                {
                    Id = user.Iduser,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    Active = user.Active,
                    SchoolId = user.SchoolIdschool,
                    School = new SchoolApiModel
                    {
                        Id = user.SchoolIdschoolNavigation.Idschool,
                        Name = user.SchoolIdschoolNavigation.Name
                    },
                    Lessons = user.Lessons.Select(l => new LessonApiModel
                    {
                        Class = new ClassApiModel
                        {
                            End = l.ClassIdclassNavigation.End,
                            Start = l.ClassIdclassNavigation.Start,
                            Id = l.ClassIdclass,
                            NumberOfClass = l.ClassIdclassNavigation.NumberOfClass,
                            Course = new CourseApiModel
                            {
                                Id = l.ClassIdclassNavigation.CourseIdcourse,
                                Name = l.ClassIdclassNavigation.CourseIdcourseNavigation.Name
                            }
                        },
                        Present = l.Present

                    }).ToList(),
                    Roles = user.UserRoles.Select(ur => new RoleApiModel
                    {
                        Id = ur.RoleIdrole,
                        Name = ur.RoleIdroleNavigation.Role1
                    }).ToList()
                };

                await transaction.CommitAsync();

                return result;
            }
        }
    }
}
