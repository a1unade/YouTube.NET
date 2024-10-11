using Xunit;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Features.UserRequests.CheckUserEmail;
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
        var handler = new CheckUserEmailHandler(UserRepository.Object, EmailService.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ReturnTrueForNewUser()
    {
        
        var request = new Request
        {
            Email = User.Email!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(UserRepository.Object, EmailService.Object);

        await Assert.ThrowsAsync<BadRequestException>(async () => { await handler.Handle(command, default); });
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
        var handler = new CheckUserEmailHandler(UserRepository.Object, EmailService.Object);

        await Assert.ThrowsAsync<BadRequestException>(async () => { await handler.Handle(command, default); });
    }
}