using Xunit;
using YouTube.Application.Features.Email.CodeCheck;
using Request = YouTube.Application.Common.Requests.Email.CodeCheckRequest;

namespace YouTube.UnitTests.CommandsTests.CodeCheckRequest;

public class PostCodeCheckHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CodeCheckHandler_Success()
    {
        var request = new Request
        {
            Id = User.Id,
            Code = "123456"
        };

        var command = new CodeCheckCommand(request);
        var handler = new CodeCheckHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccessfully);
    }
}