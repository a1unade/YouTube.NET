using Microsoft.AspNetCore.Http;
using YouTube.Application.DTOs.Video;

namespace YouTube.Application.Interfaces;

/// <summary>
/// Взаимоодействие с файловым хранилищем
/// </summary>
public interface IS3Service
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="content">Параметры файла</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Путь до файла в S3</returns>
    Task<string> UploadAsync(FileContent content, CancellationToken cancellationToken);

    /// <summary>
    /// Получить ссылку на файл
    /// </summary>
    /// <param name="bucketName">Название бакета</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>Ссылка на файл</returns>
    Task<string> GetFileUrlAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Получить поток 
    /// </summary>
    /// <param name="bucketName">Бакет</param>
    /// <param name="fileName">Название файл</param>
    /// <param name="contentType">контент тайп</param>
    /// <param name="response">Http response</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    Task StreamFileAsync(string bucketName, string fileName, string contentType, HttpResponse response,
        CancellationToken cancellationToken);
}