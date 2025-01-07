using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Interfaces;

/// <summary>
/// Сервис для работы с файлами
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Загрузить файл с метаданными
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="userId">Ид пользователя</param>
    /// <param name="metadata">Метаданные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Ид файла</returns>
    Task<Guid> UploadFileAndMetadataAsync(IFormFile file, Guid userId, Dictionary<string, string> metadata, CancellationToken cancellationToken);
    
    /// <summary>
    /// Переместить файл в постоянное хранилище
    /// </summary>
    /// <param name="fileId">Ид файла</param>
    /// <param name="userId">Ид пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task MoveToPermanentStorageAsync(Guid fileId, Guid userId, CancellationToken cancellationToken);

}