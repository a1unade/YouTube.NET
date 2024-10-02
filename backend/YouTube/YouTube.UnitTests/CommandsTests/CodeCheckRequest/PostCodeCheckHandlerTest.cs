using Xunit;
using YouTube.Application.Features.Email.CodeCheckForConfirmEmail;
using Request = YouTube.Application.Common.Requests.Email.CodeCheckRequest;

namespace YouTube.UnitTests.CommandsTests.CodeCheckRequest;
[Collection("Sequential Tests")]
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

        var command = new CodeCheckForConfirmEmailCommand(request);
        var handler = new CodeCheckForConfirmEmailHandler(EmailService.Object, UserManager.Object, UserRepository.Object);

        var result = await handler.Handle(command, default);
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccessfully);
    }
}