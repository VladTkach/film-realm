using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);

    Task<User> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto);
    Task<UserDto> UpdateUserPassword(UpdateUserPassword updateUserPassword);
}