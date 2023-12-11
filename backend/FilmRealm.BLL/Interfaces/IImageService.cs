using FilmRealm.Common.DTOs.User;
using Microsoft.AspNetCore.Http;

namespace FilmRealm.BLL.Interfaces;

public interface IImageService
{
    Task<UserDto> AddAvatarAsync(IFormFile avatar);
    Task<Guid> AddPosterAsync(IFormFile poster, Guid? currentImageId);
    Task DeleteAvatarAsync();
    Task DeletePosterAsync(Guid posterId);
}