﻿using AutoMapper;
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

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return _mapper.Map<UserDto>(await userRepository.GetByIdAsync(id));
    }

    public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
    {
        if (await GetUserByEmailAsync(createUserDto.Email) is not null)
        {
            throw new EmailAlreadyUsedException();
        }

        var user = _mapper.Map<User>(createUserDto);
        
        PreparePassword(user, createUserDto.Password);
        user.UserRoleId = 1;
        user.UserRole.Name = "User";
        await userRepository.AddAsync(user);

        return user;
    }

    public async Task<UserDto> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto)
    {
        var user = await userRepository.GetByIdAsync(updateUserNameDto.Id);
        
        if (await userRepository.GetUserByNameAsync(updateUserNameDto.UserName) is not null)
        {
            throw new UserNameAlreadyUsedException();
        }

        user.UserName = updateUserNameDto.UserName;
        userRepository.Update(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserPasswordAsync(UpdateUserPassword updateUserPassword)
    {
        var user = await userRepository.GetByIdAsync(updateUserPassword.Id);
        if (!SecurityHelper.PasswordValidation(updateUserPassword.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            throw new InvalidPasswordException();
        }
        
        PreparePassword(user, updateUserPassword.NewPassword);
         
        userRepository.Update(user);
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllAdminsAsync()
    {
        return _mapper.Map<List<UserDto>>(await userRepository.GetAllAdminsAsync());
    }

    public async Task<List<UserDto>> GetUsersAsync(string userName)
    {
        return _mapper.Map<List<UserDto>>(await userRepository.GetUsersByNameAsync(userName));
    }

    public async Task DeleteAdminAsync(int id)
    {
        var user = await userRepository.GetUserInternalById(id);
        user.UserRole.Name = "User";
        userRepository.Update(user);
    }

    public async Task<UserDto> PromoteUserAsync(int id)
    {
        var user = await userRepository.GetUserInternalById(id);
        user.UserRole.Name = "Admin";
        userRepository.Update(user);
        return _mapper.Map<UserDto>(user);
    }

    private void PreparePassword(User user, string password)
    {
        var salt = SecurityHelper.GenerateSalt();
        user.PasswordSalt = salt;
        user.PasswordHash = SecurityHelper.HashPassword(password, salt);
    }
}