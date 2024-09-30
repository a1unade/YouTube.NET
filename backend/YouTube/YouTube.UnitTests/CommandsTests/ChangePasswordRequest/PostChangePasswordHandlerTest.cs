using Xunit;
using YouTube.Application.Features.User.ChangePassword;
using Request = YouTube.Application.Common.Requests.User.ChangePasswordRequest;
namespace YouTube.UnitTests.CommandsTests.ChangePasswordRequest;


public class PostChangePasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ChangePasswordHandler_Success()
    {
        var request = new Request
        {
            Password = "fwafaf",
            Email = "bulatfri18@gmail.com"
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}