using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public interface IAuthenticationService<TUserAuthentication> where TUserAuthentication : class
    {
        Task<bool> AuthenticateUserAsync(TUserAuthentication userAuthentication, string hashedPassword);
    }
}
