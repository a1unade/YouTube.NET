using Xunit;
using YouTube.Application.Common.Requests.Email;
using YouTube.Application.Features.Email.CodeCheckForChangePassword;

namespace YouTube.UnitTests.CommandsTests.CodeCheckForChangePassword;

public class PostCodeCheckForChangePasswordHandlerTest : TestCommandBase
{
    [Fact]
    public async Task CodeCheckForChangePasswordHandler_Success()
    {
        var request = new CodeCheckForChangePasswordRequest
        {
            Code = "123456",
            Email = User.Email!
        };

        var command = new CodeCheckForChangePasswordCommand(request);
        var handler = new CodeCheckForChangePasswordHandler(UserRepository.Object, UserManager.Object);

        var response = await handler.Handle(command, default);
        
        Assert.NotNull(response);
        Assert.True(response.IsSuccessfully);
        Assert.Equal(User.Id, response.Id);
    }
}