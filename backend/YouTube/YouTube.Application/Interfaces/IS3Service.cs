using YouTube.Application.DTOs.Video;

namespace YouTube.Application.Interfaces;

public interface IS3Service
{
    Task<string> UploadAsync(FileContent content, CancellationToken cancellationToken);

    Task<string> GetLinkAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    Task<string> GetObjectAsync(string bucketName, string fileName, CancellationToken cancellationToken);
    
}