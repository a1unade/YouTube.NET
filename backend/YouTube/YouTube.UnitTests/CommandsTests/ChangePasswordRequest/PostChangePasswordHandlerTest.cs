using Xunit;
using YouTube.Application.Features.User.ChangePassword;
using Request = YouTube.Application.Common.Requests.User.ChangePasswordRequest;
namespace YouTube.UnitTests.CommandsTests.ChangePasswordRequest;

[Collection("Sequential Tests")]
public class PostChangePasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ChangePasswordHandler_Success()
    {
        var request = new Request
        {
            Password = "fwafaf",
            Id = User.Id
        };

        var command = new ChangePasswordCommand(request);
        var handler = new ChangePasswordHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}