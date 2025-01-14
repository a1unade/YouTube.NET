using Microsoft.AspNetCore.Http;
using YouTube.Application.DTOs.File;

namespace YouTube.Application.Interfaces;

/// <summary>
/// Сервис для работы с файлами
/// </summary>
public interface IFileService
{
    Task<FileIdWithPath> UploadFileOnPermanentStorage(Guid fileId, Guid userId, IFormFile file,  CancellationToken cancellationToken);


    /// <summary>
    /// Переместить файл в постоянное хранилище
    /// </summary>
    /// <param name="metadata">Метаданные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task<string> MoveToPermanentStorageAsync(MetadataDto metadata, CancellationToken cancellationToken);

}