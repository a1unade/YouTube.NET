using Xunit;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Logout;

namespace YouTube.UnitTests.CommandsTests.AuthRequest.LogoutTest;

public class PostLogoutHandlerTest : TestCommandBase
{
    [Fact]
    public async Task LogoutHandler_Success()
    {
        var request = new LogoutRequest
        {
            UserId = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc")
        };

        var command = new LogoutCommand(request);
        var handler = new LogoutHandler(SignInManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.Equal(true, result.IsSuccessfully);
    }
}