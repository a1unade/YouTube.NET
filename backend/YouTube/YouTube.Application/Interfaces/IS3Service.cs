using Microsoft.AspNetCore.Http;
using YouTube.Application.DTOs.File;
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
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    Task<Stream> GetFileStreamAsync(string bucketName, string fileName, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить файл из хранидища
    /// </summary>
    /// <param name="bucketName">Бакет</param>
    /// <param name="fileName">Название файл</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Удалить файл</returns>
    Task RemoveFileAsync(string bucketName, string fileName, CancellationToken cancellationToken);
}