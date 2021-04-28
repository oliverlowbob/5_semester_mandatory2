using Skoleprotokol.ApiModels;
using Skoleprotokol.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Repository
{
    public class UserRepository
    {

        public async Task EnableUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                if (user.Active == true)
                {
                    return;
                }

                user.Active = true;

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        public async Task DisableUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                if (user.Active == false)
                {
                    return;
                }

                user.Active = false;

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

        public async Task UpdateUser(UserApiModel args)
        {

            if (args == null)
            {
                throw new ArgumentNullException("No args given");
            }

            if (args.Id == 0)
            {
                throw new ArgumentNullException("No user id");
            }

            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(args.Id);

                if (!String.IsNullOrEmpty(args.FirstName))
                {
                    user.FirstName = args.FirstName;
                }
                if (!String.IsNullOrEmpty(args.LastName))
                {
                    user.LastName = args.LastName;
                }
                if (!String.IsNullOrEmpty(args.Email))
                {
                    user.Email = args.Email;
                }
                if (!String.IsNullOrEmpty(args.Password))
                {
                    user.Password = args.Password;
                }

                await dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
        }

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