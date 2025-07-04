using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.DTOs.File;
using YouTube.Application.Interfaces;
using YouTube.Domain.ClickHouseEntity;

namespace YouTube.Application.Features.Video.UploadVideo;

public class UploadVideoHandler : IRequestHandler<UploadVideoCommand, BaseResponse>
{
    private readonly IDbContext _context;
    private readonly IS3Service _s3Service;
    private readonly IClickHouseService _clickHouseService;

    public UploadVideoHandler(IDbContext context, IS3Service s3Service, IClickHouseService clickHouseService)
    {
        _context = context;
        _s3Service = s3Service;
        _clickHouseService = clickHouseService;
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

        var videoFile = new Domain.Entities.File();
        var previewFile = new Domain.Entities.File();

        foreach (var file in request.Files)
        {
            if (string.IsNullOrWhiteSpace(file.FileName))
                throw new ArgumentNullException(nameof(file.FileName));

            if (file.Length <= 0)
                throw new ArgumentException("Некорректное количество байт");

            var fileId = Guid.NewGuid();
            var path = await _s3Service.UploadAsync(new FileContent
            {
                Content = file.OpenReadStream(),
                FileName = fileId.ToString(),
                ContentType = file.ContentType,
                Length = file.Length,
                Bucket = channel.Id.ToString()
            }, cancellationToken);

            var fileToDb = new Domain.Entities.File
            {
                Id = fileId,
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

        var video = new Domain.Entities.Video
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            ReleaseDate = DateOnly.FromDateTime(DateTime.Today),
            IsHidden = request.IsHidden,
            Channel = channel,
            PreviewImg = previewFile,
            VideoUrl = videoFile,
            Category = category
        };
        await _context.Videos.AddAsync(video, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        
        await _clickHouseService.AddData(new View
        {
            Id = Guid.NewGuid(),
            VideoName = request.Name,
            VideoId = video.Id,
            ChannelId = channel.Id,
            ViewCount = 0
        }, cancellationToken);

        return new BaseResponse { IsSuccessfully = true };
    }
}