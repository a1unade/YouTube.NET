using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.Channel.GetChannel;
using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

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

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(query, default);
        });
        Assert.Equal(ChannelErrorMessage.ChannelNotFound, exception.Message);
    }

    [Fact]
    public async Task GetChannelHandlerById_ReturnsCorrectUrls_WhenBannerImgAndMainImgFileExist()
    {
        UserChannel.BannerImg = new File
        {
            Path = "image.jpg",
            FileName = "image.jpg",
            BucketName = "image.jpg"
        };
        UserChannel.MainImgFile = new File
        {
            Path = "image.jpg",
            FileName = "image.jpg",
            BucketName = "image.jpg"
        };
        await Context.SaveChangesAsync();

        var request = new IdRequest { Id = UserChannel.Id };
        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

        S3Service.Setup(s => s.GetFileUrlAsync(It.IsAny<string>(), It.IsAny<string>(), default))
            .ReturnsAsync("image.jpg");

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.Equal("image.jpg", response.Channel.BannerUrl);
        Assert.Equal("image.jpg", response.Channel.MainImgUrl);
    }

    [Fact]
    public async Task GetChannelHandlerById_ReturnsNullUrls_WhenImagesDoNotExist()
    {
        var channel = new Channel
        {
            Name = "fafwWfw",
            User = User,
            MainImgId = null,
            MainImgFile = null,
            BannerImgId = null,
            BannerImg = null,
            Country = "fawfaw"
        };

        await Context.Channels.AddAsync(channel);
        await Context.SaveChangesAsync();

        var request = new IdRequest { Id = channel.Id };
        var query = new GetChannelQuery(request);
        var handler = new GetChannelQueryHandler(Context, S3Service.Object);

        S3Service.Setup(s => s.GetFileUrlAsync(It.IsAny<string>(), It.IsAny<string>(), default))
            .ReturnsAsync((string)null!);

        var response = await handler.Handle(query, default);

        Assert.True(response.IsSuccessfully);
        Assert.Equal(String.Empty, response.Channel.BannerUrl);
        Assert.Equal(String.Empty, response.Channel.MainImgUrl);
    }
}