using FilmRealm.BlobStorage.Models;

namespace FilmRealm.BlobStorage.Interfaces;

public interface IBlobService
{
    Task UploadAsync(string containerName, BlobDto blobDto);
    Task UpdateAsync(string containerName, BlobDto blobDto);
    Task<BlobDto> DownloadAsync(string containerName, string blobId);
    Task<bool> DeleteAsync(string containerName, string blobId);
}