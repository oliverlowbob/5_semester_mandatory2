using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class AuthenticationService : IAuthenticationService<UserAuthenticationDto>
    {
        public async Task<bool> AuthenticateUserAsync(UserAuthenticationDto userAuthentication, string hashedPassword)
        {
            return await Task.Run(() =>
            {
                return BCrypt.Net.BCrypt.Verify(userAuthentication.Password, hashedPassword);
            });
        }
    }
}
