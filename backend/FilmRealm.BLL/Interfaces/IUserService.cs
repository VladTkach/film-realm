using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;

namespace FilmRealm.BLL.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<UserDto> GetUserByIdAsync(int id);

    Task<User> CreateUserAsync(CreateUserDto createUserDto);
    Task<UserDto> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto);
    Task<UserDto> UpdateUserPasswordAsync(UpdateUserPassword updateUserPassword);

    Task<List<UserDto>> GetAllAdminsAsync();
    Task<List<UserDto>> GetUsersAsync(string userName);

    Task DeleteAdminAsync(int id);
    Task<UserDto> PromoteUserAsync(int id);
}