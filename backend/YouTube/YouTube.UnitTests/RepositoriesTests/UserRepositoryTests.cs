using System.Text.Json;
using Moq;
using Xunit;
using YouTube.Persistence.Repositories;

namespace YouTube.UnitTests.RepositoriesTests;

public class UserRepositoryTests : TestCommandBase
{
    private readonly UserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userRepository = new UserRepository(Context, Cache.Object);
    }
    
    [Fact]
    public async Task UserRepository_ReturnUser_GetUserByEmail()
    {
        var user = await _userRepository.FindByEmail("bulatfri18@gmail.com", default);
        Assert.NotNull(user);
    }
    
    [Fact]
    public async Task UserRepository_ReturnNull_GetUserByEmail()
    {
        var user = await _userRepository.FindByEmail("2fw18@gmail.com", default);
        Assert.Null(user);
    }

    [Fact]
    public async Task UserRepository_ReturnUser_GetUserById()
    {
        var user = await _userRepository.FindById(Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"), default);
        Assert.NotNull(user);
    }
    
    [Fact]
    public async Task UserRepository_ReturnUserFromCache_GetUserById()
    {
        Cache.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonSerializer.SerializeToUtf8Bytes(User));
        var user = await _userRepository.FindById(Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"), default);
        Assert.NotNull(user);
        Cache.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task UserRepository_ReturnNull_GetUserById()
    {
        var user = await _userRepository.FindById(Guid.Parse("18b88d2e-0b9e-41bd-8f44-2bd2c3fd04c2"), default);
        Assert.Null(user);
    }
}