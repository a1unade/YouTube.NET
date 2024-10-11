using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Application.Features.Video.UploadVideo;

public class UploadVideoHandler : IRequestHandler<UploadVideoCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;

    public UploadVideoHandler(IDbContext context, IS3Service s3Service)
    {
        _context = context;
        _s3Service = s3Service;
    }

    public async Task<BaseResponse> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
    {
        if (request.ChannelId == Guid.Empty || request is null || string.IsNullOrEmpty(request.Name)
            || request.Files.Count != 2)
            throw new ValidationException();

        var channel = await _context.Channels.FindAsync(request.ChannelId, cancellationToken);

        if (channel is null)
            throw new NotFoundException(ChannelErrorMessage.ChannelNotFound);

        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Name == request.Category, cancellationToken);

        File videoFile = new File();
        File previewFile = new File();

        foreach (var file in request.Files)
        {
            if (string.IsNullOrWhiteSpace(file.FileName))
                throw new ArgumentNullException(nameof(file.FileName));

            if (file.Length <= 0)
                throw new ArgumentException("Некорректное количество байт");
            
            var path = await _s3Service.UploadAsync(new FileContent
            {
                Content = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Lenght = file.Length,
                Bucket = channel.Id.ToString()
            }, cancellationToken);

            var fileToDb = new File
            {
                Size = file.Length,
                ContentType = file.ContentType,
                Path = path,
                FileName = file.FileName,
                BucketName = channel.Id.ToString()
            };
            
            await _context.Files.AddAsync(fileToDb, cancellationToken);

            if (file.ContentType.StartsWith("video/"))
                videoFile = fileToDb;
            else
                previewFile = fileToDb;
        }

        await _context.Videos.AddAsync(new Domain.Entities.Video
        {
            Name = request.Name,
            Description = request.Description,
            ReleaseDate = DateOnly.FromDateTime(DateTime.Today),
            IsHidden = request.IsHidden,
            Channel = channel,
            PreviewImg = previewFile,
            VideoUrl = videoFile,
            Category = category
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new BaseResponse { IsSuccessfully = true };
    }
}