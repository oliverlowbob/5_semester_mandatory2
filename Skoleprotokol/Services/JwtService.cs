using Microsoft.IdentityModel.Tokens;
using Skoleprotokol.Dtos;
using Skoleprotokol.Utils;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skoleprotokol.Services
{
    public class JwtService : IJwtService<UserDto>
    {
        private readonly JwtOptions _options;

        public JwtService(JwtOptions options)
        {
            _options = options;
        }

        public string GenerateAccessToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecretKey = Encoding.UTF8.GetBytes(_options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName)
                }),
                Expires = DateTime.UtcNow.AddMilliseconds(_options.ExpiresInMilliseconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
