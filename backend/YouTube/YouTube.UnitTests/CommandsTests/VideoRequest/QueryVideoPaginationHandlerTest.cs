using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Features.Video.GetVideoPagination;
using YouTube.Domain.Entities;
using File = System.IO.File;

namespace YouTube.UnitTests.CommandsTests.VideoRequest;

[Collection("VideoPaginationHandlerTest")]
[CollectionDefinition("VideoPaginationHandlerTest", DisableParallelization = true)]
public class QueryVideoPaginationHandlerTest : TestCommandBase
{
    [Fact]
    public async Task VideoPaginationHandler_Success()
    {
        var request = new VideoPaginationRequest
        {
            Page = 1,
            Size = 4,
            Category = null,
            Sort = null
        };

        var query = new GetVideoPaginationQuery(request);
        var handler = new GetVideoPaginationQueryHandler(S3Service.Object, VideoRepository.Object);

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Videos);
        Assert.True(response.Videos.Count > 1);
    }

    [Fact]
    public async Task VideoPaginationHandler_ShouldReturnChannelImage_WhenMainImgFileIsNotNull()
    {
        var request = new VideoPaginationRequest
        {
            Page = 1,
            Size = 4,
            Category = null,
            Sort = null
        };

        var query = new GetVideoPaginationQuery(request);

        var video = new Video
        {
            Id = Guid.NewGuid(),
            Name = "Test Video",
            ViewCount = 100,
            ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
            ChannelId = Guid.NewGuid(),
            Channel = new Channel
            {
                MainImgFile = new Domain.Entities.File
                {
                    BucketName = "TestBucket",
                    FileName = "TestFileName"
                },
                Name = "Test"
            },
            PreviewImgId = Guid.NewGuid(),
            PreviewImg = new Domain.Entities.File
            {
                Id = Guid.NewGuid(),
                Size = 12,
                ContentType = "fawfa",
                Path = "fawfawfwa",
                FileName = "fawfawfawfwa",
                BucketName = "fawfawfawhfabhf"
            }
        };

        VideoRepository.Setup(v => v.GetVideoPagination(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Video> { video });

        S3Service.Setup(s => s.GetFileUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("TestUrl");

        var handler = new GetVideoPaginationQueryHandler(S3Service.Object, VideoRepository.Object);

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Videos);
        Assert.Equal("TestUrl", response.Videos.First().ChannelImageUrl);
    }

    [Fact]
    public async Task VideoPaginationHandler_ShouldReturnEmptyChannelImage_WhenMainImgFileIsNull()
    {
        var request = new VideoPaginationRequest
        {
            Page = 1,
            Size = 4,
            Category = null,
            Sort = null
        };

        var query = new GetVideoPaginationQuery(request);

        var video = new Video
        {
            Id = Guid.NewGuid(),
            Name = "Test Video",
            ViewCount = 100,
            ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
            ChannelId = Guid.NewGuid(),
            Channel = new Channel
            {
                MainImgFile = null,
                Name = "Chaaneel"
            },
            PreviewImgId = Guid.NewGuid(),
            PreviewImg = new Domain.Entities.File
            {
                Id = Guid.NewGuid(),
                Size = 12,
                ContentType = "fawfa",
                Path = "fawfawfwa",
                FileName = "fawfawfawfwa",
                BucketName = "fawfawfawhfabhf"
            }
        };

        VideoRepository.Setup(v => v.GetVideoPagination(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Video> { video });

        var handler = new GetVideoPaginationQueryHandler(S3Service.Object, VideoRepository.Object);

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Videos);
        Assert.Empty(response.Videos.First().ChannelImageUrl);
    }


    [Fact]
    public async Task VideoPaginationHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new VideoPaginationRequest
        {
            Page = -1,
            Size = 4,
            Category = null,
            Sort = null
        };

        var query = new GetVideoPaginationQuery(request);
        var handler = new GetVideoPaginationQueryHandler(S3Service.Object, VideoRepository.Object);


        await Assert.ThrowsAsync<ValidationException>(async () => { await handler.Handle(query, default); });
    }
}