using System.Security.Claims;
using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.Email.CodeCheck;
using YouTube.Domain.Entities;
using Handler = YouTube.Application.Features.Email.CodeCheck.CodeCheckHandler;

namespace YouTube.UnitTests.CommandsTests.CodeCheckRequest;

public class CodeCheckHandlerThrowExceptionTests : TestCommandBase
{
    [Fact]
    public async Task CodeCheckHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Application.Common.Requests.Email.CodeCheckRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Code = ""
        };

        var command = new CodeCheckCommand(request);
        var handler = new Handler(EmailService.Object, UserManager.Object, UserRepository.Object);

        await Assert.ThrowsAsync<ValidationException>(async () => { await handler.Handle(command, default); });
    }

    [Fact]
    public async Task CodeCheckHandler_ThrowUserNotFoundExceptionException_ForInvalidId()
    {
        var request = new Application.Common.Requests.Email.CodeCheckRequest
        {
            Id = Guid.NewGuid(),
            Code = "123456"
        };

        var command = new CodeCheckCommand(request);
        var handler = new Handler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }

    [Fact]
    public async Task CodeCheckHandler_ThrowBadRequestException_ForClaimIsEmpty()
    {
        UserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(() => new List<Claim>());
        var request = new Application.Common.Requests.Email.CodeCheckRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Code = "123456"
        };

        var command = new CodeCheckCommand(request);
        var handler = new Handler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(AnyErrorMessage.ClaimsIsEmpty, exception.Message);
        UserManager.Verify(x => x.GetClaimsAsync(It.IsAny<User>()), Times.Once);
    }
    
    [Fact]
    public async Task CodeCheckHandler_ThrowBadRequestException_ForInvalidConfirmationCode()
    {
        var request = new Application.Common.Requests.Email.CodeCheckRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Code = "999999"
        };

        var command = new CodeCheckCommand(request);
        var handler = new Handler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(AnyErrorMessage.InvalidConfirmationCode, exception.Message);
    }
}