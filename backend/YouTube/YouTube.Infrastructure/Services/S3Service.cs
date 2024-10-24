using Minio;
using Minio.DataModel.Args;
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

    public async Task<string> GetFileUrlAsync(string bucketName, string fileName,
        CancellationToken cancellationToken)
    {
        try
        {
            var link = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithExpiry(60 * 60 * 24))
                .ConfigureAwait(false);

            return link ?? string.Empty;
        }
        catch(Exception)
        {
            return string.Empty;
        }
        
    }

    public async Task<string> GetObjectAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var bucket = new BucketExistsArgs()
            .WithBucket(bucketName);

        bool bucketExist = await _minioClient.BucketExistsAsync(bucket, cancellationToken);

        if (!bucketExist)
            return "Bucket not found";

        // Обратный вызов для обработки потока данных
        await _minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var content = reader.ReadToEnd();
                            //Console.WriteLine(content); // Выводим данные в консоль или обрабатываем их
                        }
                    }),
                cancellationToken)
            .ConfigureAwait(false);

        return "Object processed";
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