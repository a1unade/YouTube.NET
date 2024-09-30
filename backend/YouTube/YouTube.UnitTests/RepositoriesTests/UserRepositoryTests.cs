using Xunit;

namespace YouTube.UnitTests.RepositoriesTests;

public class UserRepositoryTests : TestCommandBase
{
    [Fact]
    public async Task UserRepository_ReturnUser_GetUserById()
    {
        var user = await UserRepository.Object.FindById(Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"), default);
        Assert.NotNull(user);
        Assert.Equal("Ilya", user.UserName);
    }
    
    [Fact]
    public async Task UserRepository_ReturnNull_GetUserById()
    {
        var user = await UserRepository.Object.FindById(Guid.Parse("53afbb05-bb2d-25e0-8bef-489ef1cd6fdc"), default);
        Assert.Null(user);
    }
    
    [Fact]
    public async Task UserRepository_ReturnUser_GetUserByEmail()
    {
        var user = await UserRepository.Object.FindByEmail("bulatfri18@gmail.com", default);
        Assert.NotNull(user);
    }
    
    [Fact]
    public async Task UserRepository_ReturnNull_GetUserByEmail()
    {
        var user = await UserRepository.Object.FindByEmail("2fw18@gmail.com", default);
        Assert.Null(user);
    }
}