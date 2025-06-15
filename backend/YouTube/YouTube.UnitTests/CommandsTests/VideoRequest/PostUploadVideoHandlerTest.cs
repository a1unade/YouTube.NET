using Microsoft.AspNetCore.Http;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Features.Video.UploadVideo;

namespace YouTube.UnitTests.CommandsTests.VideoRequest;
[Collection("UploadVideoHandlerTest")]
[CollectionDefinition("UploadVideoHandlerTest", DisableParallelization = true)]
public class PostUploadVideoHandlerTest : TestCommandBase
{
    [Fact]
    public async Task UploadVideoHandler_Success()
    {
        var videoData1 = new byte[] { 1, 2, 3, 41, 23, 2 };
        var videoData2 = new byte[] { 23, 2, 1, 24, 5, 2 };

        var request = new UploadVideoRequest
        {
            Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(videoData1), 0, videoData1.Length, "file1.mp4", "file1.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "video/mp4" } }
                },
                new FormFile(new MemoryStream(videoData2), 0, videoData2.Length, "file2.mp4", "file2.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } }
                }
            },
            ChannelId = UserChannel.Id,
            Name = "Fwafawfawfa",
            Description = "fawfawfmakfmawf",
            Category = "fawfaff",
            IsHidden = false
        };

        var command = new UploadVideoCommand(request);
        var handler = new UploadVideoHandler(Context, S3Service.Object, ClickHouseService.Object);
        var result = await handler.Handle(command, default);

        Assert.True(result.IsSuccessfully);
    }

    [Fact]
    public async Task UploadVideoHandler_ThrowValidationException_ForInvalidRequest()
    {
        var videoData1 = new byte[] { 1, 2, 3, 41, 23, 2 };
        var videoData2 = new byte[] { 23, 2, 1, 24, 5, 2 };

        var request = new UploadVideoRequest
        {
            Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(videoData1), 0, videoData1.Length, "file1.mp4", "file1.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "video/mp4" } }
                },
                new FormFile(new MemoryStream(videoData2), 0, videoData2.Length, "file2.mp4", "file2.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } }
                }
            },
            ChannelId = Context.Channels.ToList()[0].Id,
            Name = "",
            Description = "fawfawfmakfmawf",
            Category = "fawfaff",
            IsHidden = false
        };

        var command = new UploadVideoCommand(request);
        var handler = new UploadVideoHandler(Context, S3Service.Object, ClickHouseService.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task UploadVideoHandler_ThrowNotFoundException_ForInvalidChannelId()
    {
        var videoData1 = new byte[] { 1, 2, 3, 41, 23, 2 };
        var videoData2 = new byte[] { 23, 2, 1, 24, 5, 2 };

        var request = new UploadVideoRequest
        {
            Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(videoData1), 0, videoData1.Length, "file1.mp4", "file1.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "video/mp4" } }
                },
                new FormFile(new MemoryStream(videoData2), 0, videoData2.Length, "file2.mp4", "file2.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } }
                }
            },
            ChannelId = Guid.NewGuid(),
            Name = "fawfawf",
            Description = "fawfawfmakfmawf",
            Category = "fawfaff",
            IsHidden = false
        };

        var command = new UploadVideoCommand(request);
        var handler = new UploadVideoHandler(Context, S3Service.Object, ClickHouseService.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(ChannelErrorMessage.ChannelNotFound, exception.Message);
    }
    
    [Fact]
    public async Task UploadVideoHandler_ThrowArgumentNullException_ForInvalidFileName()
    {
        var videoData1 = new byte[] { 1, 2, 3, 41, 23, 2 };
        var videoData2 = new byte[] { 23, 2, 1, 24, 5, 2 };

        var request = new UploadVideoRequest
        {
            Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(videoData1), 0, videoData1.Length, "", "")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "video/mp4" } }
                },
                new FormFile(new MemoryStream(videoData2), 0, videoData2.Length, "file2.mp4", "file2.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } }
                }
            },
            ChannelId = Context.Channels.ToList()[0].Id,
            Name = "fawfawf",
            Description = "fawfawfmakfmawf",
            Category = "fawfaff",
            IsHidden = false
        };

        var command = new UploadVideoCommand(request);
        var handler = new UploadVideoHandler(Context, S3Service.Object, ClickHouseService.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task UploadVideoHandler_ThrowArgumentException_ForInvalidBytes()
    {
        var videoData1 = new byte[] {  };
        var videoData2 = new byte[] { 23, 2, 1, 24, 5, 2 };

        var request = new UploadVideoRequest
        {
            Files = new List<IFormFile>
            {
                new FormFile(new MemoryStream(videoData1), 0, videoData1.Length, "fawfwa.mp4", "faww.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "video/mp4" } }
                },
                new FormFile(new MemoryStream(videoData2), 0, videoData2.Length, "file2.mp4", "file2.mp4")
                {
                    Headers = new HeaderDictionary { { "Content-Type", "image/jpeg" } }
                }
            },
            ChannelId = Context.Channels.ToList()[0].Id,
            Name = "fawfawf",
            Description = "fawfawfmakfmawf",
            Category = "fawfaff",
            IsHidden = false
        };

        var command = new UploadVideoCommand(request);
        var handler = new UploadVideoHandler(Context, S3Service.Object, ClickHouseService.Object);

        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
}