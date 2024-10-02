using Xunit;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.User.ConfirmEmail;

namespace YouTube.UnitTests.CommandsTests.ConfirmEmailRequest;
[Collection("Sequential Tests")]
public class PostConfirmEmailHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ConfirmEmailHandler_Success()
    {
        var request = new IdRequest
        {
            Id = User.Id
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        var response = await handler.Handle(command,default);

        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}