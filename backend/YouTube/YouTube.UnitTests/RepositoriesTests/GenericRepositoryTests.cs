using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using YouTube.Domain.Entities;

namespace YouTube.UnitTests.RepositoriesTests;
[Collection("RepositoryTest")]
public class GenericRepositoryTests : TestCommandBase
{    
    [Fact]
    public async Task GenericRepository_GetAllUser_Success()
    {
        var users = await GenericRepository.Object.GetAll().ToListAsync();

        Assert.Single(users);
    }

    [Fact]
    public async Task GenericRepository_GetById_Success()
    {
        var user = await GenericRepository.Object.GetById(User.Id, default);

        Assert.Equal(user, User);
    }

    [Fact]
    public void GenericRepository_GetByPredicate_Success()
    {
        var expectedUserName = "Ilya";

        var result = GenericRepository.Object.Get(user => user.UserName == expectedUserName);

        var user = result.FirstOrDefault();
        Assert.NotNull(user);
        Assert.Equal(expectedUserName, user.UserName);
    }

    [Fact]
    public async Task GenericRepository_Add_Success()
    {
        var entity = new User
        {
            UserName = "Igor",
            Email = "Gfwfawfy@gmail.com",
            PasswordHash = "GAkfawfmat123fmwf"
        };

        await GenericRepository.Object.Add(entity, default);

        GenericRepository.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GenericRepository_Remove_Success()
    {
        var user = new User
        {
            UserName = "Pasha",
            Email = "pasha@gmail.com",
            EmailConfirmed = false,
            PasswordHash = "GAt123fmwf"
        };
        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();
        
        await GenericRepository.Object.Remove(user, default);

        GenericRepository.Verify(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GenericRepository_RemoveById_Success()
    {
        var user = new User
        {
            UserName = "Kirill",
            Email = "Kir@gmail.com",
            EmailConfirmed = false,
            PasswordHash = "GAt123fmwf"
        };
        await Context.Users.AddAsync(user);
        await Context.SaveChangesAsync();
        
        await GenericRepository.Object.RemoveById(user.Id, default);

        GenericRepository.Verify(x => x.RemoveById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}