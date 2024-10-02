using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.User;
using YouTube.Application.Features.Email.ForgotPasswordSendEmail;

namespace YouTube.UnitTests.CommandsTests.ForgotPasswordSendEmailRequest;
[Collection("Sequential Tests")]
public class ForgotPasswordHandlerThrowExceptionTests : TestCommandBase
{
    [Fact]
    public async Task ForgotPasswordHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new EmailRequest
        {
            Email = ""
        };
        
        var command = new ForgotPasswordSendEmailCommand(request);
        var handler =
            new ForgotPasswordSendEmailHandler(EmailService.Object, UserRepository.Object, UserManager.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task ForgotPasswordHandler_ThrowNotFoundException_ForInvalidEmail()
    {
        var request = new EmailRequest
        {
            Email = "fawfnjwjnfjanwf"
        };
        
        var command = new ForgotPasswordSendEmailCommand(request);
        var handler =
            new ForgotPasswordSendEmailHandler(EmailService.Object, UserRepository.Object, UserManager.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
}