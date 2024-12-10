using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.Files;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Files.GetFileStream;

public class GetFileStreamQueryHandler : IRequestHandler<GetFileStreamQuery, FileStreamResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;

    public GetFileStreamQueryHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }
    
    public async Task<FileStreamResponse> Handle(GetFileStreamQuery request, CancellationToken cancellationToken)
    {
        var file = await _context.Files
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                    ?? throw new NotFoundException();
        
        var stream = await _s3Service.GetFileStreamAsync(file.BucketName, file.FileName, cancellationToken);

        return new FileStreamResponse
        {
            IsSuccessfully = true,
            EntityId = file.Id,
            Stream = stream,
            FileName = file.FileName,
            ContentType = file.ContentType!
        };
    }
}