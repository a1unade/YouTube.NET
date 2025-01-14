using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.DTOs.File;
using YouTube.Application.Interfaces;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IDistributedCache _cache;
    private readonly IS3Service _s3Service;
    private readonly IDbContext _context;
    private readonly string _tempBucketName = "temp-bucket";

    public FileService(IDistributedCache cache, IS3Service s3Service, IDbContext context)
    {
        _cache = cache;
        _s3Service = s3Service;
        _context = context;
    }

    public async Task<FileIdWithPath> UploadFileOnPermanentStorage(Guid fileId, Guid userId, IFormFile file,
        CancellationToken cancellationToken)
    {
        var cacheDataJson = await _cache.GetStringAsync($"file:{fileId}", cancellationToken);

        if (string.IsNullOrEmpty(cacheDataJson))
            throw new NotFoundException("Метаданные файла не найдены в Redis.");

        var cacheData = JsonConvert.DeserializeObject<CacheMetadataDto>(cacheDataJson)
                        ?? throw new InvalidOperationException();

        if (file.Length != cacheData.Metadata.Size)
            throw new InvalidOperationException("Размер файла не совпадает с метаданными.");

        var path = await _s3Service.UploadAsync(new FileContent
        {
            Content = file.OpenReadStream(),
            FileName = fileId.ToString(),
            ContentType = file.ContentType,
            Length = file.Length,
            Bucket = _tempBucketName
        }, cancellationToken);

        cacheData.Counter++;

        var updateCacheMetadata = JsonConvert.SerializeObject(cacheData);
        await _cache.SetStringAsync($"file:{fileId}", updateCacheMetadata, cancellationToken);

        if (cacheData.Counter == 2)
        {
            var newPath = await MoveToPermanentStorageAsync(cacheData.Metadata, cancellationToken);
            return new FileIdWithPath { FileId = fileId, Path = newPath };
        }

        var fileEntity = new File
        {
            Id = fileId,
            Size = cacheData.Metadata.Size,
            ContentType = cacheData.Metadata.ContentType,
            Path = path,
            FileName = cacheData.Metadata.FileName,
            BucketName = _tempBucketName
        };
        
        await _context.Files.AddAsync(fileEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new FileIdWithPath { FileId = fileId, Path = path };
    }

    public async Task<string> MoveToPermanentStorageAsync(MetadataDto metadata, CancellationToken cancellationToken)
    {
        var sourceFileStream =
            await _s3Service.GetFileStreamAsync(_tempBucketName, metadata.FileId.ToString(), cancellationToken);

        var fileContent = new FileContent
        {
            Bucket = metadata.UserId.ToString(),
            FileName = metadata.FileId.ToString(),
            Content = sourceFileStream,
            Length = sourceFileStream.Length,
            ContentType = metadata.ContentType
        };

        var newPath = await _s3Service.UploadAsync(fileContent, cancellationToken);

        await _s3Service.RemoveFileAsync(_tempBucketName, metadata.FileId.ToString(), cancellationToken);

        await _cache.RemoveAsync($"file:{metadata.FileId}", cancellationToken);
        var fileEntity = new File
        {
            Id = metadata.FileId,
            Size = metadata.Size,
            ContentType = metadata.ContentType,
            Path = newPath,
            FileName = metadata.FileName,
            BucketName = metadata.UserId.ToString()
        };

        await _context.Files.AddAsync(fileEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newPath;
    }
}