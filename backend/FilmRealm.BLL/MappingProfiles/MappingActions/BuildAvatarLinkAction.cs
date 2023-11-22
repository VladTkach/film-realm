using AutoMapper;
using Azure.Storage.Blobs;
using FilmRealm.BlobStorage.Models;
using FilmRealm.Common.DTOs.User;
using FilmRealm.DAL.Entities;
using Microsoft.Extensions.Options;

namespace FilmRealm.BLL.MappingProfiles.MappingActions;

public class BuildAvatarLinkAction : IMappingAction<User, UserDto>
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
        destination.AvatarUrl = BuildLink(source.AvatarId);
    }

    private string? BuildLink(Guid? avatarUrl)
    {
        return avatarUrl is not null
            ? $"{_blobServiceClient.Uri.AbsoluteUri}/{_blobStorageOptions.ImagesContainer}/{avatarUrl}"
            : null;
    }
}