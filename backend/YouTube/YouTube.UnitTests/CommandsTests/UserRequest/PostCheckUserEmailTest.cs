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
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ReturnTrueForNewUser()
    {
        var request = new Request
        {
            Email = "fawfawf@gmail.com"
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.NewUser);
        Assert.True(response.IsSuccessfully);
    }
    
    [Fact]
    public async Task CheckUserEmailHandler_ReturnUserEmailIsConfirmed()
    {
        User.EmailConfirmed = true;
        await Context.SaveChangesAsync();
        var request = new Request
        {
            Email = User.Email!
        };

        var command = new CheckUserEmailCommand(request);
        var handler = new CheckUserEmailHandler(EmailService.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.True(response.Confirmation);
        Assert.True(response.IsSuccessfully);
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