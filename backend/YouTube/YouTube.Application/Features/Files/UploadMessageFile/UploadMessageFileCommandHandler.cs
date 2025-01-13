using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.File;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Files.UploadMessageFile;

public class UploadMessageFileCommandHandler : IRequestHandler<UploadMessageFileCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;

    public UploadMessageFileCommandHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }
    
    public async Task<BaseResponse> Handle(UploadMessageFileCommand request, CancellationToken cancellationToken)
    {
        if (request.File.Length <= 0)
            throw new ValidationException();
        
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                   ?? throw new NotFoundException(UserErrorMessage.UserNotFound);

        var path = await _s3Service.UploadAsync(new FileContent
        {
            Content = request.File.OpenReadStream(),
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            Length = request.File.Length,
            Bucket = user.Id.ToString()
        }, cancellationToken);

        var file = new Domain.Entities.File
        {
            Size = request.File.Length,
            ContentType = request.File.ContentType,
            Path = path,
            FileName = request.File.FileName,
            BucketName = user.Id.ToString()
        };

        await _context.Files.AddAsync(file, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            EntityId = file.Id
        };
    }
}