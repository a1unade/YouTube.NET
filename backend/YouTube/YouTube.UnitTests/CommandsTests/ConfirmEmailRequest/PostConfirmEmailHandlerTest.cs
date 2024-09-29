using Xunit;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.User.ConfirmEmail;

namespace YouTube.UnitTests.CommandsTests.ConfirmEmailRequest;

public class PostConfirmEmailHandlerTest : TestCommandBase
{
    [Fact]
    public async Task ConfirmEmailHandler_Success()
    {
        var request = new EmailConfirmRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Email = "bulatfri18@gmail.com"
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);
        var response = await handler.Handle(command,default);

        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
    }
}