using Microsoft.IdentityModel.Tokens;
using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class JwtService : IJwtService<UserDto>
    {
        private readonly string _tokenSecret;

        public JwtService(string tokenSecret)
        {
            _tokenSecret = tokenSecret;
        }

        public string GenerateAccessToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecretKey = Encoding.ASCII.GetBytes(_tokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
