using Moq;
using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Features.UserRequests.CheckUserEmail;
using YouTube.Domain.Entities;
using Request = YouTube.Application.Common.Requests.User.EmailRequest;
namespace YouTube.UnitTests.CommandsTests.UserRequest;
[Collection("CheckUserEmailTest")]
[CollectionDefinition("CheckUserEmailTest", DisableParallelization = true)]
public class PostCheckUserEmailTest : TestCommandBase
{
    [Fact]
    public async Task CheckUserEmailHandler_Success()
    {
        User.EmailConfirmed = false;
        await Context.SaveChangesAsync();
        var request = new Request
        {
            Email = User.Email!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ReturnTrueForNewUser()
    {
        UserRepository.Setup(x => x.FindByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);
        
        var request = new Request
        {
            Email = User.Email!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.NewUser);
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ThrowBadRequestException_ForUserEmailIsConfirmed()
    {
        User.EmailConfirmed = true;
        await Context.SaveChangesAsync();
        var request = new Request
        {
            Email = User.Email!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        await Assert.ThrowsAsync<BadRequestException>(async () => { await handler.Handle(command, default); });
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ThrowValidationException_ForInvalidRequest()
    {
        
        var request = new Request
        {
            Email = null!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        await Assert.ThrowsAsync<ValidationException>(async () => { await handler.Handle(command, default); });
    }
}