using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Files.AddFileWithMetadata;

public class AddFileWithMetadataCommandHandler : IRequestHandler<AddFileWithMetadataCommand, BaseResponse>
{
    private readonly IFileService _service;

    public AddFileWithMetadataCommandHandler(IFileService service)
    {
        _service = service;
    }
    
    public async Task<BaseResponse> Handle(AddFileWithMetadataCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length <= 0 || request.Metadata == null)
            throw new ValidationException();

        var fileId = await _service.UploadFileAndMetadataAsync(
            request.File,
            request.UserId,
            request.Metadata,
            cancellationToken);
        
        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = fileId
        };
    }
}