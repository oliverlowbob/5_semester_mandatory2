using Skoleprotokol.Dtos;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class AuthenticationService : IAuthenticationService<UserLoginDto>
    {
        public async Task<bool> AuthenticateUserAsync(UserLoginDto userLoginCredentials, string hashedPassword)
        {
            return await Task.Run(() =>
            {
                return BCrypt.Net.BCrypt.Verify(userLoginCredentials.Password, hashedPassword);
            });
        }
    }
}
