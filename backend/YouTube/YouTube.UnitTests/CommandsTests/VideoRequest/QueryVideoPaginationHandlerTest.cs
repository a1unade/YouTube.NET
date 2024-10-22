using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Video;
using YouTube.Application.Features.Video.GetVideoPagination;

namespace YouTube.UnitTests.CommandsTests.VideoRequest;

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


        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(query, default);
        });
    }
}