using System.Security.Claims;
using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Features.Email.CodeCheckForConfirmEmail;
using YouTube.Domain.Entities;
using Request = YouTube.Application.Common.Requests.Email.CodeCheckRequest;

namespace YouTube.UnitTests.CommandsTests.EmailRequest;
[Collection("CodeCheckHandlerTest")]
[CollectionDefinition("CodeCheckHandlerTest", DisableParallelization = true)]
public class PostCodeCheckHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CodeCheckHandler_Success()
    {
        var request = new Request
        {
            Id = User.Id,
            Code = "123456"
        };

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccessfully);
    }
    
    [Fact]
    public async Task CodeCheckHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new Application.Common.Requests.Email.CodeCheckRequest
        {
            Id = User.Id,
            Code = ""
        };

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

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

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

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
            Id = User.Id,
            Code = "123456"
        };

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

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
            Id = User.Id,
            Code = "999999"
        };

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });

        Assert.Equal(AnyErrorMessage.InvalidConfirmationCode, exception.Message);
    }
}