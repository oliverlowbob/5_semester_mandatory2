using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using System;
using System.Collections.Generic;
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

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntity = await context.Users.FirstOrDefaultAsync(u => u.Iduser == userId);
                var userDto = _mapper.Map<UserDto>(userEntity);
                return userDto;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntity = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
                var userDto = _mapper.Map<UserDto>(userEntity);
                return userDto;
            }
        }

        public async Task<bool> UpdateUserByIdAsync(int userId, UserDto user)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntity = await context.Users.FirstOrDefaultAsync(u => u.Iduser == userId);

                if (userEntity != null)
                {
                    context.Update(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntities = await context.Users.ToListAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(userEntities);
                return userDtos;
            }
        }
    }
}
