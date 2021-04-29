using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IAuthenticationService<TUserAuthCreds> where TUserAuthCreds : class
    {
        Task<bool> AuthenticateUserAsync(TUserAuthCreds userLoginCredentials, string hashedPassword);
    }
}
