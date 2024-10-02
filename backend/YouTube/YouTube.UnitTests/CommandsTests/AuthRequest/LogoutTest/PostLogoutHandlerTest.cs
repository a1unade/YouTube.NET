using Xunit;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Logout;

namespace YouTube.UnitTests.CommandsTests.AuthRequest.LogoutTest;
[Collection("Sequential Tests")]
public class PostLogoutHandlerTest : TestCommandBase
{
    [Fact]
    public async Task LogoutHandler_Success()
    {
        var request = new LogoutRequest
        {
            UserId = User.Id
        };

        var command = new LogoutCommand(request);
        var handler = new LogoutHandler(SignInManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.True(result.IsSuccessfully);
    }
}