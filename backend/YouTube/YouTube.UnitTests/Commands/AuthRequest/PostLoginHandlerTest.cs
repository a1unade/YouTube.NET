using Moq;
using Xunit;
using YouTube.Application.Common.Requests.Auth;
using YouTube.Application.Features.Auth.Login;
using YouTube.Domain.Entities;

namespace YouTube.UnitTests.Commands.AuthRequest;

public class PostLoginHandlerTest : TestCommandBase
{
    [Fact]
    public async Task LoginHandler_Success()
    {
        var request = new LoginRequest
        {
            Email = "bulatfri18@gmail.com",
            Password = "Ilya1337"
        };
    
        var command = new LoginCommand(request);
        var handler = new LoginHandler(UserManager.Object, UserRepository.Object, JwtGenerator.Object,
            SignInManager.Object);
    
        var response = await handler.Handle(command, default);
    
        Assert.NotNull(response);
        Assert.NotNull(response.Token);
        Assert.Equal(true, response.IsSuccessfully);
        UserManager.Verify(x => x.CheckPasswordAsync(It.IsAny<User>(), "Ilya1337"), Times.Once);
    }
}