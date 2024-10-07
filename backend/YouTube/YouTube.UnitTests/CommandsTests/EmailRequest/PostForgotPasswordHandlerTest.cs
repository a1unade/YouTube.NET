using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.Email.ForgotPasswordSendEmail;
using Request = YouTube.Application.Common.Requests.User.EmailRequest;

namespace YouTube.UnitTests.CommandsTests.EmailRequest;
[Collection("ForgotPasswordHandlerTest")]
[CollectionDefinition("ForgotPasswordHandlerTest", DisableParallelization = true)]

public class PostForgotPasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ForgotPasswordHandler_Success()
    {
        var request = new Request
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
    
    [Fact]
    public async Task ForgotPasswordHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Request
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
        var request = new Request
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