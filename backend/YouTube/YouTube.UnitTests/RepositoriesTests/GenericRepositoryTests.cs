using Microsoft.EntityFrameworkCore;
using Xunit;
using YouTube.Domain.Entities;
using YouTube.Persistence.Repositories;

namespace YouTube.UnitTests.RepositoriesTests;
[Collection("GenericRepository")]
public class GenericRepositoryTests : TestCommandBase
{ 
    private readonly GenericRepository<User> _genericRepository;
    
    public GenericRepositoryTests()
    {
        _genericRepository = new GenericRepository<User>(Context);
    }
    
    [Fact]
    public async Task GenericRepository_GetAllUser_Success()
    {
        var users = await _genericRepository.GetAll().ToListAsync();

        Assert.Single(users);
    }

    [Fact]
    public async Task GenericRepository_GetById_Success()
    {
        var user = await _genericRepository.GetById(User.Id, default);

        Assert.Equal(user, User);
    }

    [Fact]
    public void GenericRepository_GetByPredicate_Success()
    {
        var expectedUserName = "Ilya";

        var result = _genericRepository.Get(user => user.UserName == expectedUserName);

        var user = result.FirstOrDefault();
        Assert.NotNull(user);
        Assert.Equal(expectedUserName, user.UserName);
    }
    
    [Fact]
    public async Task GenericRepository_RemoveById_Success()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Kirill",
            Email = "Kir@gmail.com",
            EmailConfirmed = false,
            PasswordHash = "GAt123fmwf"
        };

        await _genericRepository.Add(user,default);

        await _genericRepository.RemoveById(user.Id, default);


        var users = await Context.Users.ToListAsync();

        Assert.Single(users);
    }
}