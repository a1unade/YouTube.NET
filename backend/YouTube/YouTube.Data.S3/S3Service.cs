using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using YouTube.Application.Interfaces;

namespace YouTube.Data.S3;

public class S3Service : IS3Service
{
    private readonly IMinioClient _minioClient;

    public S3Service(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadAsync(IFormFile file, string bucketName, CancellationToken cancellationToken)
    {
        await BucketExistAsync(bucketName, cancellationToken);

        await using (var stream = file.OpenReadStream())
        {
            var uploadFile = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(file.FileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

            await _minioClient.PutObjectAsync(uploadFile, cancellationToken);
        }

        return file.FileName;
    }

    public async Task<string> GetLinkAsync(string bucketName, string fileName,
        CancellationToken cancellationToken)
    {
        var link = await _minioClient.PresignedGetObjectAsync(
                new PresignedGetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithExpiry(300))
            .ConfigureAwait(false) ?? String.Empty;

        return link;
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