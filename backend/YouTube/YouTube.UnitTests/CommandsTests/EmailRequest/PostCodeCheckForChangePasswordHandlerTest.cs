using System.Security.Claims;
using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.Email.CodeCheckForChangePassword;
using YouTube.Domain.Entities;

namespace YouTube.UnitTests.CommandsTests.EmailRequest;
[Collection("CodeCheckForChangePasswordHandlerTest")]
[CollectionDefinition("CodeCheckForChangePasswordHandlerTest", DisableParallelization = true)]
public class PostCodeCheckForChangePasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_Success()
    {
        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "123456",
            Email = User.Email!
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
        Assert.Equal(User.Id, response.Id);
    }
    
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_ThrowValidationException_ForInvalidRequest()
    {
        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "",
            Email = User.Email!
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserManager.Object);

        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            await handler.Handle(command, default);
        });
    }
    
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_ThrowNotFoundException_ForInvalidEmail()
    {
        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "12345",
            Email = "fjefeufehfe@gmail.com"
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserManager.Object);

        var exception = await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(UserErrorMessage.UserNotFound, exception.Message);
    }
    
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_ThrowBadRequestException_ForClaimsIsEmpty()
    {
        UserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(() => new List<Claim>());

        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "12345",
            Email = User.Email!
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserManager.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(AnyErrorMessage.ClaimsIsEmpty, exception.Message);
        UserManager.Verify(x => x.GetClaimsAsync(It.IsAny<User>()), Times.Once);
    }
    
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_ThrowBadRequestException_ForInvalidConfirmationCode()
    {
        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "123",
            Email = User.Email!
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserManager.Object);

        var exception = await Assert.ThrowsAsync<BadRequestException>(async () =>
        {
            await handler.Handle(command, default);
        });
        
        Assert.Equal(AnyErrorMessage.InvalidConfirmationCode, exception.Message);
    }
}