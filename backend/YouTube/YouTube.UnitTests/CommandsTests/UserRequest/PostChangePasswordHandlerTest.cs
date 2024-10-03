using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.User.ChangePassword;

namespace YouTube.UnitTests.CommandsTests.UserRequest;
using Request = Application.Common.Requests.User.ChangePasswordRequest;

[Collection("HandlerTest")]
public class PostChangePasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ChangePasswordHandler_Success()
    {
        var request = new Request
        {
            Password = "fwafaf",
            Id = User.Id
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task ChangePasswordHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Request
        {
            Password = "",
            Id = User.Id
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task ChangePasswordHandler_ThrowUserNotFoundException_ForInvalidId()
    {
        var request = new Request
        {
            Password = "fwfawfawf",
            Id = Guid.NewGuid()
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
}