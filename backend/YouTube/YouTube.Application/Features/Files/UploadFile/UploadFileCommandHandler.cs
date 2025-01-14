using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Files.UploadFile;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, BaseResponse>
{
    private readonly IFileService _service;

    public UploadFileCommandHandler(IFileService service)
    {
        _service = service;
    }
    
    public async Task<BaseResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length <= 0)
            throw new ValidationException("Размер 0");

        var fileIdWithPathDto = await _service.UploadFileOnPermanentStorage(request.FileId, request.UserId, request.File, cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            Message = $"Файл находится по пути : {fileIdWithPathDto.Path}",
            EntityId = fileIdWithPathDto.FileId
        };
    }
}