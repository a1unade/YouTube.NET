using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Interfaces;

/// <summary>
/// Сервис для работы с файлами
/// </summary>
public interface IFileService
{
    Task<string> UploadFileOnPermanentStorage(Guid fileId, IFormFile file, CancellationToken cancellationToken);

    
    /// <summary>
    /// Переместить файл в постоянное хранилище
    /// </summary>
    /// <param name="fileId">Ид файла</param>
    /// <param name="userId">Ид пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<string> MoveToPermanentStorageAsync(Guid fileId, Guid userId, CancellationToken cancellationToken);

}