using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FilmRealm.BlobStorage.Interfaces;
using FilmRealm.BlobStorage.Models;

namespace FilmRealm.BlobStorage.Services;

public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
{
    public async Task UploadAsync(string containerName, BlobDto blobDto)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blobDto.Name);

        if (await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"BlobDto with id:{blobDto.Name} already exists.");
        }

        var blobHttpHeader = new BlobHttpHeaders { ContentType = blobDto.ContentType };
        await blobClient.UploadAsync(new BinaryData(blobDto.Content ?? new byte[] { }),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });
    }

    public async Task UpdateAsync(string containerName, BlobDto blobDto)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blobDto.Name);

        if (!await blobClient.ExistsAsync())
        {
            throw new InvalidOperationException($"Blob with id:{blobDto.Name} does not exist.");
        }

        var blobHttpHeader = new BlobHttpHeaders { ContentType = blobDto.ContentType };
        await blobClient.UploadAsync(new BinaryData(blobDto.Content ?? new byte[] { }),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });
    }

    public Task<BlobDto> DownloadAsync(string containerName, string blobId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(string containerName, string blobId)
    {
        var blobClient = await GetBlobClientInternalAsync(containerName, blobId);

        return await blobClient.DeleteIfExistsAsync();;
    }

    private async Task<BlobClient> GetBlobClientInternalAsync(string containerName, string blobName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        
        return containerClient.GetBlobClient(blobName);
    }
}