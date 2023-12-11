using AutoMapper;
using Azure.Storage.Blobs;
using FilmRealm.BlobStorage.Models;
using FilmRealm.Common.DTOs.Film;
using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;
using Microsoft.Extensions.Options;

namespace FilmRealm.BLL.MappingProfiles.MappingActions;

public class BuildAvatarLinkAction : IMappingAction<User, UserDto>, IMappingAction<Film, FilmDto>
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobStorageOptions _blobStorageOptions;

    public BuildAvatarLinkAction(BlobServiceClient blobServiceClient, IOptions<BlobStorageOptions> blobStorageOptions)
    {
        _blobServiceClient = blobServiceClient;
        _blobStorageOptions = blobStorageOptions.Value;
    }

    public void Process(User source, UserDto destination, ResolutionContext context)
    {
        destination.AvatarUrl = BuildLink(source.AvatarId, _blobStorageOptions.ImagesContainer);
    }

    public void Process(Film source, FilmDto destination, ResolutionContext context)
    {
        destination.PosterUrl = BuildLink(source.PosterId, _blobStorageOptions.PostersContainer) ?? string.Empty;
    }

    private string? BuildLink(Guid? avatarUrl, string container)
    {
        return avatarUrl is not null
            ? $"{_blobServiceClient.Uri.AbsoluteUri}/{container}/{avatarUrl}"
            : null;
    }
}