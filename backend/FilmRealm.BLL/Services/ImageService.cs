﻿using FilmRealm.BLL.Interfaces;
using FilmRealm.BlobStorage.Interfaces;
using FilmRealm.BlobStorage.Models;
using FilmRealm.DAL.Context;
using FilmRealm.DAL.Entities;
using FilmRealm.DAL.Interfaces;
using FilmRealm.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace FilmRealm.BLL.Services;

public class ImageService : IImageService
{
    private const int Megabyte = 1024 * 1024;
    private const int MaxFileLength = 5 * Megabyte;
    private readonly string[] _fileTypes = { "image/png", "image/jpeg" };
    private readonly IBlobService _blobStorageService;
    private readonly IUserIdGetter _userIdGetter;
    private readonly IUserRepository _userRepository;
    private readonly BlobStorageOptions _blobStorageOptions;

    public ImageService(IBlobService blobStorageService, IUserIdGetter userIdGetter,
        IOptions<BlobStorageOptions> blobStorageOptions, IUserRepository userRepository)
    {
        _blobStorageService = blobStorageService;
        _userIdGetter = userIdGetter;
        _userRepository = userRepository;
        _blobStorageOptions = blobStorageOptions.Value;
    }
    
    public async Task AddAvatarAsync(IFormFile avatar)
    {
        ValidateImage(avatar);

        var userEntity = await _userRepository.GetByIdAsync(_userIdGetter.GetCurrentUserId());

        var content = await CropAvatar(avatar);
        var guid = userEntity.AvatarId ?? Guid.NewGuid();
        var blob = new BlobDto
        {
            Name = guid.ToString(),
            ContentType = avatar.ContentType,
            Content = content
        };

        await (userEntity.AvatarId is null
            ? _blobStorageService.UploadAsync(_blobStorageOptions.ImagesContainer, blob)
            : _blobStorageService.UpdateAsync(_blobStorageOptions.ImagesContainer, blob));

        userEntity.AvatarId = guid;
        _userRepository.Update(userEntity);
    }

    public async Task DeleteAvatarAsync()
    {
        var userEntity = await _userRepository.GetByIdAsync(_userIdGetter.GetCurrentUserId());
        if (userEntity.AvatarId is null)
        {
            throw new NotFoundException(nameof(User.AvatarId));
        }

        await _blobStorageService
            .DeleteAsync(_blobStorageOptions.ImagesContainer, userEntity.AvatarId.Value.ToString());
        
        userEntity.AvatarId = null;
        _userRepository.Update(userEntity);
    }
    
    private async Task<byte[]> CropAvatar(IFormFile avatar)
    {
        await using var imageStream = avatar.OpenReadStream();
        using var image = await Image.LoadAsync(imageStream);

        var smallerDimension = Math.Min(image.Width, image.Height);
        image.Mutate(x => x.Crop(smallerDimension, smallerDimension));

        using var ms = new MemoryStream();
        await image.SaveAsync(ms, new JpegEncoder());
        return ms.ToArray();
    }

    private void ValidateImage(IFormFile image)
    {
        if (!_fileTypes.Contains(image.ContentType))
        {
            throw new InvalidFileFormatException(string.Join(", ", _fileTypes));
        }

        if (image.Length > MaxFileLength)
        {
            throw new LargeFileException($"{MaxFileLength / Megabyte} MB");
        }
    }
}