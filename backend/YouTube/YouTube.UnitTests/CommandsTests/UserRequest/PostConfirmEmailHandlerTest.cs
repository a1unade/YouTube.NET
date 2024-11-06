using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Features.UserRequests.ConfirmEmail;

namespace YouTube.UnitTests.CommandsTests.UserRequest;
[Collection("ConfirmEmailHandlerTest")]
[CollectionDefinition("ConfirmEmailHandlerTest", DisableParallelization = true)]
public class PostConfirmEmailHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ConfirmEmailHandler_Success()
    {
        var request = new Application.Common.Requests.User.EmailRequest
        {
            Email = User.Email!
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, EmailService.Object);
        var response = await handler.Handle(command,default);

        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task ConfirmEmailHandler_ThrowsValidationException_ForInvalidIdRequest()
    {
        var request = new Application.Common.Requests.User.EmailRequest
        {
            Email = ""
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, EmailService.Object);
        
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }

    [Fact]
    public async Task ConfirmEmailHandler_ThrowsNotFoundException_ForInvalidIdRequest()
    {
        var request = new Application.Common.Requests.User.EmailRequest
        {
            Email = "fkoawkfwkaofok@gmail.com"
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, EmailService.Object);
        
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
}