namespace FilmRealm.BlobStorage.Models;

public class BlobDto
{
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = "octet-stream";
    public byte[]? Content { get; set; }
}