using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using System;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class UserService : IUserService<UserDto>
    {
        private readonly IDbContextFactory<SchoolProtocolContext> _contextFactory;
        private readonly IMapper _mapper;

        public UserService(IDbContextFactory<SchoolProtocolContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var user = await context.Users.FindAsync(userId);
                var userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
        }

        public Task<UserDto> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserById(int userId, UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
