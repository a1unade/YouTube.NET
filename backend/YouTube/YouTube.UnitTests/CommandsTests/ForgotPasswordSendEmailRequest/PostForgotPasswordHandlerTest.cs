using Xunit;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.Email.ForgotPasswordSendEmail;

namespace YouTube.UnitTests.CommandsTests.ForgotPasswordSendEmailRequest;

public class PostForgotPasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ForgotPasswordHandler_Success()
    {
        var request = new EmailRequest
        {
            Email = User.Email!
        };

        var command = new ForgotPasswordSendEmailCommand(request);
        var handler =
            new ForgotPasswordSendEmailHandler(EmailService.Object, UserRepository.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}