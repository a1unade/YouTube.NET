using Xunit;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.User.ConfirmEmail;

namespace YouTube.UnitTests.CommandsTests.ConfirmEmailRequest;

public class PostConfirmEmailHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ConfirmEmailHandler_Success()
    {
        var request = new EmailConfirmRequest
        {
            Id = User.Id,
            Email = User.Email!
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        var response = await handler.Handle(command,default);

        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}