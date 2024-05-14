using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.CommentResponse;
using YouTube.Application.Common.Responses.VideoResponse;
using YouTube.Application.DTOs.Comment;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class VideoService : IVideoService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IVideoRepository _repository;
    private readonly IYandexService _yandexService;

    public VideoService(ICommentRepository commentRepository,
        IVideoRepository repository,
        IYandexService yandexService)
    {
        _commentRepository = commentRepository;
        _repository = repository;
        _yandexService = yandexService;
    }

    public async Task<VideoCommentListResponse> GetVideoCommentList(int id, CancellationToken cancellationToken)
    {
        var comments = await _commentRepository.GetVideoComment(id, cancellationToken);

        if (comments.Count == 0)
            return new VideoCommentListResponse()
            {
                IsSuccessfully = true,
                Message = "0 Comments"
            };

        var result = new List<CommentItem>();

        foreach (var comment in comments)
        {
            result.Add(new CommentItem()
            {
                CommentText = comment.CommentText,
                Id = comment.Id,
                DisLikeCount = comment.DisLikeCount,
                LikeCount = comment.LikeCount,
                PostDate = comment.PostDate,
                UserName = comment.UserInfo.Name
            });
        }

        return new VideoCommentListResponse()
        {
            IsSuccessfully = true,
            Comments = result
        };
    }


    public async Task<BaseResponse> AddComment(AddCommentDto request, CancellationToken cancellationToken)
    {
        await _commentRepository.AddComment(request.CommentText, request.VideoId, request.UserId, cancellationToken);
        return new BaseResponse()
        {
            IsSuccessfully = true
        };
    }

    public async Task<VideoListResponse> GetRandomVideo(CancellationToken cancellationToken)
    {
        var res = await _repository.GetRandomVideo(cancellationToken);

        if (res.Count == 0)
            return new VideoListResponse();


        var t = res.Select(x => new VideoItem()
        {
            DisLikeCount = x.DisLikeCount,
            LikeCount = x.LikeCount,
            Description = x.Description,
            Id = x.Id,
            Name = x.Name,
            PreviewImg = x.StaticFile.Path,
            ReleaseDate = x.ReleaseDate,
            ViewCount = x.ViewCount
        }).ToList();
        
        return new VideoListResponse()
        {
            IsSuccessfully = true,
            VideoItems = t
        };
    }

    public async Task<VideoResponse> GetVideoById(int id, CancellationToken cancellationToken)
    {
        var res = await _repository.GetById(id, cancellationToken);

        if (res == null)
            return new VideoResponse()
            {
                IsSuccessfully = false,
                Error = new List<string>() { "Not Found" }
            };
       
        var yandex = await _yandexService.GetFile(res.PathInDisk, cancellationToken);
      
        if (yandex.IsSuccessfully)
        {
            return new VideoResponse()
            {
                IsSuccessfully = true,
                VideoItem = new VideoItem()
                {
                    DisLikeCount = res.DisLikeCount,
                    Description = res.Description,
                    LikeCount = res.LikeCount,
                    Name = res.Name,
                    ViewCount = res.ViewCount,
                    Id = res.Id,
                    ReleaseDate = res.ReleaseDate,
                    PreviewImg = res.StaticFile.Path,
                    Href = "http://localhost:5041/static/videos" + res.PathInDisk
                }
            };
        }

        return new VideoResponse()
        {
            Error = new List<string>() { "Что то пошло не так" }
        };
    }
}