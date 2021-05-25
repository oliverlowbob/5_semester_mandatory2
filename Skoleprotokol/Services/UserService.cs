using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Skoleprotokol.Data;
using Skoleprotokol.Dtos;
using Skoleprotokol.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skoleprotokol.Services
{
    public class UserService : IUserService<UserDto, NewUserDto>
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

        public async Task<UserDto> UpdateUserByIdAsync(int id, UserDto user)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntity = await context.Users.FirstOrDefaultAsync(u => u.Iduser == id);

                if (userEntity != null)
                {
                    if (!string.IsNullOrEmpty(user.FirstName))
                    {
                        userEntity.FirstName = user.FirstName;
                    }

                    if (!string.IsNullOrEmpty(user.LastName))
                    {
                        userEntity.LastName = user.LastName;
                    }

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        userEntity.Email = user.Email;
                    }

                    if (user.Active.HasValue)
                    {
                        userEntity.Active = user.Active;
                    }

                    await context.SaveChangesAsync();

                    return _mapper.Map<UserDto>(userEntity);
                }
                return null;
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

        public async Task<bool> CreateNewUser(NewUserDto user)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var userEntity = _mapper.Map<User>(user);

                // Hash user password
                userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password, 12);

                await context.Users.AddAsync(userEntity);
                return await context.SaveChangesAsync() == 1;
            }
        }
    }
}
