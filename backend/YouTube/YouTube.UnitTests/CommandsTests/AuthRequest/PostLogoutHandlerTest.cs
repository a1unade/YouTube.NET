using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Logout;

namespace YouTube.UnitTests.CommandsTests.AuthRequest;
[Collection("HandlerTest")]
public class PostLogoutHandlerTest : TestCommandBase
{
    [Fact]
    public async Task LogoutHandler_Success()
    {
        var request = new LogoutRequest
        {
            UserId = User.Id
        };

        var command = new LogoutCommand(request);
        var handler = new LogoutHandler(SignInManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.True(result.IsSuccessfully);
    }
    
    [Fact]
    public async Task LogoutHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new LogoutRequest
        {
            UserId = Guid.Empty
        };

        var command = new LogoutCommand(request);
        var handler = new LogoutHandler(SignInManager.Object, UserRepository.Object);


        var exception = await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserIdIsNotCorrect, exception.Message);
    }
    
    [Fact]
    public async Task LogoutHandler_ThrowUserNotFoundException_ForInvalidId()
    {
        var request = new LogoutRequest
        {
            UserId = Guid.NewGuid()
        };

        var command = new LogoutCommand(request);
        var handler = new LogoutHandler(SignInManager.Object, UserRepository.Object);


        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
}