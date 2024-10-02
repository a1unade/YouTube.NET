using Xunit;
using YouTube.Application.Features.Auth.Authorization;
 
namespace YouTube.UnitTests.CommandsTests.AuthRequest.AuthTest;
[Collection("Sequential Tests")]
public class PostAuthHandlerTest : TestCommandBase
{
    [Fact]
    public async Task AuthHandler_Success()
    {
        var request = new Application.Common.Requests.Auth.AuthRequest
        {
            Password = "Password123",
            Email = "PleshEnergy@gmail.com",
            Name = "Alehandro",
            SurName = "Pleshev",
            DateOfBirth = DateTime.Today,
            Gender = "Гау"
        };

        var command = new AuthCommand(request);
        var handler = new AuthHandler(UserManager.Object, SignInManager.Object, UserRepository.Object,
            JwtGenerator.Object, EmailService.Object, Context);
        var response = await handler.Handle(command, default);

        Assert.NotNull(response);
        Assert.NotNull(response.Token);
        Assert.True(response.IsSuccessfully);
    }
}