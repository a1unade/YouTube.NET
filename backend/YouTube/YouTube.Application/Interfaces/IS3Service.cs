using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Interfaces;

public interface IS3Service
{
    Task<string> UploadAsync(IFormFile file, string bucketName, CancellationToken cancellationToken);

    Task<string> GetDownloadLinkAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    Task<string> GetObjectAsync(string bucketName, string fileName, CancellationToken cancellationToken);
    
}