using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.Channel.GetChannel;

namespace YouTube.UnitTests.CommandsTests.ChannelRequest;

[Collection("GetChannelHandlerTest")]
[CollectionDefinition("GetChannelHandlerTest", DisableParallelization = true)]
public class QueryGetChannelHandlerTest : TestCommandBase
{
    [Fact]
    public async Task GetChannelHandlerById_Success()
    {
        var request = new IdRequest
        {
            Id = UserChannel.Id
        };

        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Channel.Name);
    }

    [Fact]
    public async Task GetChannelHandlerById_ThrowValidationException_ForInvalidId()
    {
        var request = new IdRequest
        {
            Id = Guid.Empty
        };

        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

        await Assert.ThrowsAsync<ValidationException>(async () => { await handler.Handle(query, default); });
    }
    
    [Fact]
    public async Task GetChannelHandlerById_ThrowNotFoundException_ForInvalidId()
    {
        var request = new IdRequest
        {
            Id = Guid.NewGuid()
        };

        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => { await handler.Handle(query, default); });
        Assert.Equal(ChannelErrorMessage.ChannelNotFound, exception.Message);
    }

    [Fact]
    public async Task GetChannelHandlerById_ReturnsImagesUrl_WhenImagesIsNotNull()
    {
        
        var request = new IdRequest { Id = UserChannel.Id }; 
        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

       

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Channel.BannerUrl);
        Assert.NotNull(response.Channel.MainImgUrl);
    }
    
    [Fact]
    public async Task GetChannelHandlerById_ReturnsNull_WhenImagesIsNull()
    {
        UserChannel.BannerImg = null;
        UserChannel.MainImgFile = null;
        await Context.SaveChangesAsync();
        
        var request = new IdRequest { Id = UserChannel.Id }; 
        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);
        
        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.NotNull(response.Channel.BannerUrl);
        Assert.NotNull(response.Channel.MainImgUrl);
    }
}
