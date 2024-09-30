using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.User.ChangePassword;
using YouTube.Domain.Entities;
using Request = YouTube.Application.Common.Requests.User.ChangePasswordRequest;

namespace YouTube.UnitTests.CommandsTests.ChangePasswordRequest;

public class ChangePasswordHandlerThrowExceptionTest : TestCommandBase
{
    [Fact]
    public async Task ChangePasswordHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Request
        {
            Password = "",
            Email = "bulatfri18@gmail.com"
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task ChangePasswordHandler_ThrowUserNotFoundException_ForInvalidEmail()
    {
        var request = new Request
        {
            Password = "fwfawfawf",
            Email = "tfri18@gmail.com"
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
    
    [Fact]
    public async Task ChangePasswordHandler_ThrowBadRequestException_ForInvalidEmail()
    {
        UserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());        
        
        var request = new Request
        {
            Password = "fwfawfawf",
            Email = "bulatfri18@gmail.com"
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        
        await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
}