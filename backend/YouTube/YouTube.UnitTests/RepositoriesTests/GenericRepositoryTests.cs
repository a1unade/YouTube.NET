using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using YouTube.Domain.Entities;

namespace YouTube.UnitTests.RepositoriesTests;
[Collection("GenericRepository")]
public class GenericRepositoryTests : TestCommandBase
{ 
    // private readonly GenericRepository<User> _genericRepository;
    //
    // public GenericRepositoryTests()
    // {
    //     _genericRepository = new GenericRepository<User>(Context);
    // }
    
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
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Igor",
            Email = "Gfwfawfy@gmail.com",
            PasswordHash = "GAkfawfmat123fmwf"
        };
        
        await GenericRepository.Object.Add(user, CancellationToken.None);
        
        await Context.Users.ToListAsync();
        GenericRepository.Verify(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task GenericRepository_Remove_Success()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = "Pasha",
            Email = "pasha@gmail.com",
            EmailConfirmed = false,
            PasswordHash = "GAt123fmwf"
        };
        await GenericRepository.Object.Add(user, CancellationToken.None);

        await GenericRepository.Object.Remove(user, default); 
    

        var users = await Context.Users.ToListAsync();

        Assert.Single(users);
        GenericRepository.Verify(x => x.Remove(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);

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

        await GenericRepository.Object.Add(user,default);

        await GenericRepository.Object.RemoveById(user.Id, default);


        var users = await Context.Users.ToListAsync();
        GenericRepository.Verify(x => x.RemoveById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.Single(users);
    }
}