using AutoMapper;
using FilmRealm.BLL.Interfaces;
using FilmRealm.BLL.Services.Abstract;
using FilmRealm.Common.DTOs.User;
using FilmRealm.Common.Security;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.Shared.Exceptions;

namespace FilmRealm.BLL.Services;

public class UserService(IMapper mapper, IUserRepository userRepository) : BaseService(mapper), IUserService
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (await GetUserByEmailAsync(createUserDto.Email) is not null)
        {
            throw new EmailAlreadyUsedException();
        }

        var user = _mapper.Map<User>(createUserDto);
        
        var salt = SecurityHelper.GenerateSalt();
        user.PasswordSalt = salt;
        user.PasswordHash = SecurityHelper.HashPassword(createUserDto.Password, salt);
        user.UserRoleId = 1;
        user.UserRole.Name = "User";
        await userRepository.AddAsync(user);

        return user;
    }
}