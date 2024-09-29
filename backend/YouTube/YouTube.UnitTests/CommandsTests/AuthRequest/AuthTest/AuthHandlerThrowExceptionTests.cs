using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.Auth.Authorization;
using YouTube.Domain.Entities;
using Request = YouTube.Application.Common.Requests.Auth.AuthRequest;

namespace YouTube.UnitTests.CommandsTests.AuthRequest.AuthTest;

public class AuthHandlerThrowExceptionTests : TestCommandBase
{
    [Fact]
    public async Task AuthHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Request
        {
            Password = "FAFWfwafwf24",
            Email = "fwafawf",
            Name = "fwaf",
            SurName = "",
            DateOfBirth = new DateOnly(2004, 02, 2),
            Gender = "Гау"
        };

        var command = new AuthCommand(request);
        var handler = new AuthHandler(UserManager.Object, SignInManager.Object, UserRepository.Object,
            JwtGenerator.Object, EmailService.Object, Context);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }

    [Fact]
    public async Task AuthHandler_ThrowBadRequestException_ForBusyEmail()
    {
        var request = new Request
        {
            Password = "FAFWfwafwf24",
            Email = "bulatfri18@gmail.com",
            Name = "fwaf",
            SurName = "fawwfwafw",
            DateOfBirth = new DateOnly(2004, 02, 2),
            Gender = "Гау"
        };

        var command = new AuthCommand(request);
        var handler = new AuthHandler(UserManager.Object, SignInManager.Object, UserRepository.Object,
            JwtGenerator.Object, EmailService.Object, Context);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(AuthErrorMessages.EmailIsBusy, exception.Message);
    }
    
    [Fact]
    public async Task AuthHandler_ThrowBadRequestException_ForCreateAsyncIsCrash()
    {
        UserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed());
        
        var request = new Request
        {
            Password = "FAFWfwafwf24",
            Email = "bulatfr22318@gmail.com",
            Name = "fwaffwa",
            SurName = "fawwfwafw",
            DateOfBirth = new DateOnly(2004, 02, 2),
            Gender = "Гау"
        };
    
        var command = new AuthCommand(request);
        var handler = new AuthHandler(UserManager.Object, SignInManager.Object, UserRepository.Object,
            JwtGenerator.Object, EmailService.Object, Context);
    
        await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        UserManager.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
    }
}