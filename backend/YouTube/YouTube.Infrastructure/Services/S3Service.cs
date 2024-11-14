using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Services;

public class S3Service : IS3Service
{
    private readonly IMinioClient _minioClient;

    public S3Service(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadAsync(FileContent content, CancellationToken cancellationToken)
    {
        await BucketExistAsync(content.Bucket, cancellationToken);
        
        var uploadFile = new PutObjectArgs()
            .WithBucket(content.Bucket)
            .WithObject(content.FileName)
            .WithStreamData(content.Content)
            .WithObjectSize(content.Lenght)
            .WithContentType(content.ContentType);

        await _minioClient.PutObjectAsync(uploadFile, cancellationToken)
            .ConfigureAwait(false);

        return content.Bucket + "/" + content.FileName;
    }
    
    
    public async Task<Stream> GetFileStreamAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var bucketExist = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName), cancellationToken);
        if (!bucketExist)
            throw new NotFoundException("Bucket not found");

        var memoryStream = new MemoryStream();
        await _minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(async stream =>
            {
                await stream.CopyToAsync(memoryStream, cancellationToken);
            }), cancellationToken);

        memoryStream.Position = 0; // Устанавливаем начальную позицию для чтения
        return memoryStream;
    }


    public async Task<string> GetFileUrlAsync(string bucketName, string fileName,
        CancellationToken cancellationToken)
    {
        var link = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithExpiry(60 * 60 * 24))
            .ConfigureAwait(false);
        
        return link ?? string.Empty;
    }
    
    private async Task BucketExistAsync(string bucketName, CancellationToken cancellationToken)
    {
        var bucketExist =
            await _minioClient
                .BucketExistsAsync(new BucketExistsArgs()
                    .WithBucket(bucketName), cancellationToken)
                .ConfigureAwait(false);

        if (bucketExist)
            return;

        var bucket = new MakeBucketArgs()
            .WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(bucket, cancellationToken);
    }
}