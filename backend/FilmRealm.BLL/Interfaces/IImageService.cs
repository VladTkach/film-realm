using Microsoft.AspNetCore.Http;

namespace FilmRealm.BLL.Interfaces;

public interface IImageService
{
    Task AddAvatarAsync(IFormFile avatar);
    Task DeleteAvatarAsync();
}