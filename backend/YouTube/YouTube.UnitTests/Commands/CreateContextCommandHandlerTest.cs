using Xunit;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.User.ConfirmEmail;

namespace YouTube.UnitTests.Commands;

public class CreateContextCommandHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CreateContextCommandHandler_Success()
    {
        var request = new EmailConfirmRequest
        {
            Id = "53afbb05-bb2d-45e0-8bef-489ef1cd6fdc",
            Email = "bulatfri18@gmail.com"
        };

        var command = new ConfirmEmailCommand(request);
        var handler = new ConfirmEmailHandler(UserManager.Object, UserRepository.Object, EmailService.Object);

        await handler.Handle(command,default);
        
    }
}