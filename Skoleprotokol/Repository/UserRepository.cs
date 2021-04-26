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
        public async Task UpdateUser(UserApiModel args)
        {
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
    }
}
