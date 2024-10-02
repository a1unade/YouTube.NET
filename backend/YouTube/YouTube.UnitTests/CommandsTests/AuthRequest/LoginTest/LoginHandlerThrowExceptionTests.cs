using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Login;
using YouTube.Domain.Entities;

namespace YouTube.UnitTests.CommandsTests.AuthRequest.LoginTest;
[Collection("Sequential Tests")]
public class LoginHandlerThrowExceptionTests : TestCommandBase
{
    [Fact]
    public async Task LoginHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new LoginRequest
        {
            Email = "",
            Password = "FPWFPPP@"
        };
    
        var command = new LoginCommand(request);
        var handler = new LoginHandler(UserManager.Object, UserRepository.Object, JwtGenerator.Object,
            SignInManager.Object);
    
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task LoginHandler_ThrowUserNotFoundException_ForInvalidEmail()
    {
        var request = new LoginRequest
        {
            Email = "MegaPochta@.com",
            Password = "FPWFPPP@"
        };
    
        var command = new LoginCommand(request);
        var handler = new LoginHandler(UserManager.Object, UserRepository.Object, JwtGenerator.Object,
            SignInManager.Object);
    
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(AuthErrorMessages.UserNotFound, exception.Message);
    }
    
    [Fact]
    public async Task LoginHandler_Failure_WrongPassword()
    {
        UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(false);  

        var request = new LoginRequest
        {
            Email = User.Email!,
            Password = "Password" 
        };

        var command = new LoginCommand(request);
        var handler = new LoginHandler(UserManager.Object, UserRepository.Object, JwtGenerator.Object,
            SignInManager.Object);

        
        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(AuthErrorMessages.LoginWrongPassword, exception.Message);
        UserManager.Verify(x => x.CheckPasswordAsync(It.IsAny<User>(), "Password"), Times.Once);
    }
}