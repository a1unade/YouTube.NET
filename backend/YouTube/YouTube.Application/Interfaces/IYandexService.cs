using Microsoft.AspNetCore.Http;
using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.DiskResponse;

namespace YouTube.Application.Interfaces;

public interface IYandexService
{
    Task<string> GetAllFilesOnDisk(CancellationToken cancellationToken);

    Task<UploadFileResponse> UploadFileToDisk(IFormFile file, string pathInDisk, CancellationToken cancellationToken);

    Task<CreateFolderResponse> CreateFolder(string path, CancellationToken cancellationToken);

    Task<BaseResponse> DeleteFile(string path, CancellationToken cancellationToken);

    Task<GetFileFromDiskResponse> GetFile(string path, CancellationToken cancellationToken);

    Task<GetFileFromDiskResponse> PublishFile(string path, CancellationToken cancellationToken);
}