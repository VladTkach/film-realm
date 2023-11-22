using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FilmRealm.BlobStorage.Models;
using FilmRealm.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FilmRealm.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseFilmRealmContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<FilmRealmContext>();
        context?.Database.Migrate();
    }
    
    public static void UseContainers(this IApplicationBuilder app)
    {
        var blobServiceClient = app.ApplicationServices.GetService<BlobServiceClient>();
        var blobStorageOptions = app.ApplicationServices.GetService<IOptions<BlobStorageOptions>>();

        var avatarContainerClient = blobServiceClient?.GetBlobContainerClient(blobStorageOptions?.Value.ImagesContainer);
        avatarContainerClient?.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        
        var posterContainerClient = blobServiceClient?.GetBlobContainerClient(blobStorageOptions?.Value.PostersContainer);
        posterContainerClient?.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
    }
}