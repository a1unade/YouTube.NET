using Xunit;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.Email.CodeCheck;
using Handler = YouTube.Application.Features.Email.CodeCheck.CodeCheckHandler;

namespace YouTube.UnitTests.CommandsTests.CodeCheckHandler;

public class PostCodeCheckHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CodeCheckHandler_Success()
    {
        var request = new CodeCheckRequest
        {
            Id = Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"),
            Code = "123456"
        };

        var command = new CodeCheckCommand(request);
        var handler = new Handler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccessfully);
    }
}