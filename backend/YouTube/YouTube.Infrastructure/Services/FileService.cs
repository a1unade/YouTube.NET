using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<string> UploadFileOnPermanentStorage(Guid fileId, IFormFile file, CancellationToken cancellationToken)
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
            var newPath = await MoveToPermanentStorageAsync(fileId, cacheData.Metadata.UserId, cancellationToken);
            return newPath;
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
        
        return path;
    }

    public async Task<string> MoveToPermanentStorageAsync(Guid fileId, Guid userId, CancellationToken cancellationToken)
    {
        var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken)
                   ?? throw new NotFoundException($"Файл с ID {fileId} не найден.");

        var sourceFileStream = await _s3Service.GetFileStreamAsync(_tempBucketName, fileId.ToString(), cancellationToken);

        var fileContent = new FileContent
        {
            Bucket = userId.ToString(),
            FileName = fileId.ToString(),
            Content = sourceFileStream,
            Length = sourceFileStream.Length,
            ContentType = file.ContentType!
        };

        var newPath = await _s3Service.UploadAsync(fileContent, cancellationToken);

        await _s3Service.RemoveFileAsync(_tempBucketName, fileId.ToString(), cancellationToken);

        await _cache.RemoveAsync($"file:{fileId}", cancellationToken);

        file.Path = newPath;
        file.BucketName = userId.ToString();
        await _context.SaveChangesAsync(cancellationToken);

        return newPath;
    }
}