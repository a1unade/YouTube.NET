using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.Video.GetVideo;

namespace YouTube.UnitTests.CommandsTests.VideoRequest;
[Collection("GetVideoHandlerTest")]
[CollectionDefinition("GetVideoHandlerTest", DisableParallelization = true)]
public class QueryGetVideoHandlerTest : TestCommandBase
{
    [Fact]
    public async Task GetVideoHandlerById_Success()
    {
        var request = new IdRequest
        {
            Id = UserVideo.Id
        };

        var query = new GetVideoQuery(request);
        var handler = new GetVideoQueryHandler(Context, S3Service.Object);
        var result = await handler.Handle(query, default);

        Assert.True(result.IsSuccessfully);
    }
    
    [Fact]
    public async Task GetVideoHandlerById_ThrowValidationException_ForInvalidRequest()
    {
        var request = new IdRequest
        {
            Id = Guid.Empty
        };

        var query = new GetVideoQuery(request);
        var handler = new GetVideoQueryHandler(Context, S3Service.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(query, default);
        });
    }
    
    [Fact]
    public async Task GetVideoHandlerById_ThrowNotFoundException_ForInvalidId()
    {
        var request = new IdRequest
        {
            Id = Guid.NewGuid()
        };

        var query = new GetVideoQuery(request);
        var handler = new GetVideoQueryHandler(Context, S3Service.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(query, default);
        });
        
        Assert.Equal("Видео не найдено",exception.Message);
    }
    
    [Fact]
    public async Task GetVideoHandlerById_ThrowNotFoundException_ForChannelId()
    {
        var request = new IdRequest
        {
            Id = UserVideo.Id
        };
        
        UserVideo.ChannelId = Guid.NewGuid();
        await Context.SaveChangesAsync();
        var query = new GetVideoQuery(request);
        var handler = new GetVideoQueryHandler(Context, S3Service.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(query, default);
        });
        
        Assert.Equal(ChannelErrorMessage.ChannelNotFound, exception.Message);
    }
}