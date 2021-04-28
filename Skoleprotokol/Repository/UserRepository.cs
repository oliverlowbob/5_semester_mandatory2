using Skoleprotokol.ApiModels;
using Skoleprotokol.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Skoleprotokol.Repository
{
    public class UserRepository
    {
        private readonly Mapper _mapper;

        public UserRepository()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<Config.AutoMapping>();
            });

            _mapper = new Mapper(configuration);
        }

        public async Task EnableUser(int userId)
        {
            using (var dbContext = new Scool_ProtocolContext())
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();

                var user = await dbContext.Users.FindAsync(userId);

                if (user.Active == true)
                {
                    await transaction.CommitAsync();

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
                    await transaction.CommitAsync();

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

                var result = _mapper.Map<UserApiModel>(user);

                await transaction.CommitAsync();

                return result;
            }
        }
    }
}