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
    private readonly string _uploadCounter = "upload_counter";

    public FileService(IDistributedCache cache, IS3Service s3Service, IDbContext context)
    {
        _cache = cache;
        _s3Service = s3Service;
        _context = context;
    }
    
    public async Task<Guid> UploadFileAndMetadataAsync(IFormFile file, Guid userId, Dictionary<string, string> metadata, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();
        var fileContent = new FileContent
        {
            Content = file.OpenReadStream(),
            FileName = fileId.ToString(),
            ContentType = file.FileName,
            Lenght = file.Length,
            Bucket = _tempBucketName
        };

        var path = await _s3Service.UploadAsync(fileContent, cancellationToken);
        var metadataJson = JsonConvert.SerializeObject(metadata);
        await _cache.SetStringAsync($"metadata: {fileId}", metadataJson, cancellationToken);

        var counter = await _cache.GetStringAsync(_uploadCounter, cancellationToken);
        var newCounter = counter == null ? 1 : int.Parse(counter) + 1;
         
        await _cache.SetStringAsync(_uploadCounter, newCounter.ToString(), cancellationToken);

        var fileEntity = new File
        {
            Id = fileId,
            Size = file.Length,
            ContentType = file.ContentType,
            Path = path,
            FileName = file.FileName,
            BucketName = _tempBucketName
        };
        
        await _context.Files.AddAsync(fileEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        if (newCounter == 2)
        {
            await MoveToPermanentStorageAsync(fileId, userId, cancellationToken);
            await _cache.RemoveAsync(_uploadCounter, cancellationToken);
        }

        return fileEntity.Id;
    }

    public async Task MoveToPermanentStorageAsync(Guid fileId, Guid userId, CancellationToken cancellationToken)
    {
        var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId, cancellationToken)
                   ?? throw new NotFoundException(typeof(File));
        
        var sourceFileStream = await _s3Service.GetFileStreamAsync(_tempBucketName, fileId.ToString(), cancellationToken);
        
        var fileContent = new FileContent
        {
            Bucket = userId.ToString(),
            FileName = fileId.ToString(),
            Content = sourceFileStream,
            Lenght = sourceFileStream.Length,
            ContentType = file.ContentType!
        };

        var newPath = await _s3Service.UploadAsync(fileContent, cancellationToken);

        await _s3Service.RemoveFileAsync(_tempBucketName, fileId.ToString(), cancellationToken);
        
        await _cache.RemoveAsync($"metadata:{fileId}", cancellationToken);

        file.Path = newPath;
        file.BucketName = userId.ToString();
        await _context.SaveChangesAsync(cancellationToken);
    }
}