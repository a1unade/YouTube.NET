using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.User.ConfirmEmail;

namespace YouTube.UnitTests.CommandsTests.ConfirmEmailRequest;

public class ConfirmEmailThrowExceptionTests : TestCommandBase
{
    [Fact]
    public async Task ConfirmEmailHandler_ThrowsValidationException_ForInvalidIdRequest()
    {
        var request = new EmailConfirmRequest
        {
            Id = Guid.Empty,
            Email = "bulatfree18@gmail.com"
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task ConfirmEmailHandler_ThrowsValidationException_ForInvalidEmailRequest()
    {
        var request = new EmailConfirmRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Email = ""
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task ConfirmEmailHandler_ThrowsNotFoundException_ForInvalidEmailRequest()
    {
        var request = new EmailConfirmRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Email = "NeNastoyasha9Pochta.com"
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
}