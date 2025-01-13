using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.File;

namespace YouTube.Application.Features.Files.AddMetadata;

public class AddMetadataCommandHandler : IRequestHandler<AddMetadataCommand, BaseResponse>
{
    private readonly IDistributedCache _cache;

    public AddMetadataCommandHandler(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    public async Task<BaseResponse> Handle(AddMetadataCommand request, CancellationToken cancellationToken)
    {
        if (request.Size <= 0 || string.IsNullOrWhiteSpace(request.ContentType))
            throw new ValidationException();
        
        var fileId = Guid.NewGuid();
        
        var cacheData = new CacheMetadataDto
        {
            Metadata = new MetadataDto
            {
                FileName = request.FileName,
                Size = request.Size,
                ContentType = request.ContentType,
                FileId = fileId,
                UserId = request.UserId
            },
            Counter = 1
        };

        var metadataJson = JsonConvert.SerializeObject(cacheData);
        await _cache.SetStringAsync($"file:{fileId}", metadataJson, cancellationToken);
        
        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = fileId
        };
    }
}