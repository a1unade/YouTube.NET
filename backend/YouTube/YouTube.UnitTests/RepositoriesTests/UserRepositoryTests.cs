using Microsoft.Extensions.Caching.Distributed;
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
}