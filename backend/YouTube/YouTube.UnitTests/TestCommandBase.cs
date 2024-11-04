using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using MockQueryable;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.DTOs.Video;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;
using YouTube.UnitTests.Builders;

namespace YouTube.UnitTests;

public class TestCommandBase : IDisposable
{
    private const string UserId = "53afbb05-bb2d-45e0-8bef-489ef1cd6fdc";
    
    protected readonly ApplicationDbContext Context;
    
    protected User User { get; }
    
    protected Channel UserChannel { get; }
    
    protected Video UserVideo { get; }
    
    protected Mock<IEmailService> EmailService { get; }
    
    protected Mock<IJwtGenerator> JwtGenerator { get; }
    
    protected Mock<IUserRepository> UserRepository { get; }

    protected Mock<IVideoRepository> VideoRepository { get; }
    protected Mock<IGenericRepository<User>> GenericRepository { get; }
    
    protected Mock<UserManager<User>> UserManager { get; }
    
    protected Mock<SignInManager<User>> SignInManager { get; }
    
    protected Mock<IDistributedCache> Cache { get; }
    
    protected Mock<IS3Service> S3Service { get; }
    
    protected TestCommandBase()
    {
        User = UserBuilder.CreateBuilder()
            .SetId(UserId)
            .SetUsername("Ilya")
            .SetDisplayName("Ilay")
            .SetBirthday(new DateOnly(2004, 01, 09))
            .SetEmail("bulatfri18@gmail.com")
            .SetUserInfo()
            .SetChannel()
            .SetVideoAndFiles()
            .Build();

        UserChannel = User.Channels!.ToList()[0];
        UserVideo = UserChannel.Videos.ToList()[0];
        
        Context = ContextFactory.Create(User);
        
        // Мокирование EmailService
        EmailService = new Mock<IEmailService>();
        EmailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        EmailService.Setup(x => x.GenerateRandomCode())
            .Returns("123456");

        // Мок JwtGenerator
        JwtGenerator = new Mock<IJwtGenerator>();
        JwtGenerator.Setup(x => x.GenerateToken(It.IsAny<User>()))
            .Returns("123");

        // Mok S3
        S3Service = new Mock<IS3Service>();

        S3Service.Setup(x => x.UploadAsync(It.IsAny<FileContent>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("fwlflwflw/flawflawf");

        S3Service.Setup(x => x.GetFileUrlAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("fawfwafa");

        // Мок Cache
        Cache = new Mock<IDistributedCache>();

        Cache.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((byte[]?)null);

        Cache.Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

       // Repositories
        GenericRepository = new Mock<IGenericRepository<User>>();
        
        GenericRepository.Setup(x => x.RemoveById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        GenericRepository.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(User);
        
        GenericRepository.Setup(x => x.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        GenericRepository.Setup(x => x.GetAll())
            .Returns(new List<User> {User}.AsQueryable().BuildMock());  
        
        GenericRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns((Expression<Func<User, bool>> predicate) => Context.Users.Where(predicate.Compile()).AsQueryable());
        
        // Мокирование UserRepository
        UserRepository = new Mock<IUserRepository>();
        UserRepository.Setup(x => x.FindByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken _) =>
            {
                return Context.Users.FirstOrDefault(user => user.Email == email);
            });

        VideoRepository = new Mock<IVideoRepository>();

        VideoRepository.Setup(x =>
            x.GetVideoPagination(
                It.IsAny<int>(), 
                It.IsAny<int>(), 
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Video> { UserVideo, UserVideo });


        UserRepository.Setup(x => x.FindById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken _) => { return Context.Users.FirstOrDefault(x => x.Id == id); });

        // Мокирование UserManager
        UserManager = CreateMockUserManager();
        
        UserManager.Setup(x => x.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);

        UserManager.Setup(x => x.GetClaimsAsync(It.IsAny<User>())).ReturnsAsync(() => new List<Claim>
        {
            new(EmailSuccessMessage.EmailConfirmCodeString, "123456"),
            new(EmailSuccessMessage.EmailWarning, "156"),
            new (EmailSuccessMessage.EmailChangePasswordCode, "123456")
        });

        UserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        UserManager.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("123");

        UserManager.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("1234");

        UserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) =>
            {
                return Context.Users.FirstOrDefault(x => x.Email == email);
            });

        UserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string id) =>
            {
                return Context.Users.FirstOrDefault(x => x.Id == Guid.Parse(id));
            });

        UserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        UserManager.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), "123"))
            .ReturnsAsync(IdentityResult.Success);

        UserManager.Setup(x => x.RemoveClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);

        // Мок SignInManager
        SignInManager = new Mock<SignInManager<User>>(
            UserManager.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<User>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<User>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<User>>().Object
        );

        SignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), false, It.IsAny<string>()))
            .Returns(Task.CompletedTask);
    }

    /// <summary>
    /// Метод для создания мокированного UserManager с нужными зависимостями
    /// </summary>
    /// <returns>Мок юзер менеждера</returns>
    private Mock<UserManager<User>> CreateMockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<User>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<User>>();
        var userValidators = new List<IUserValidator<User>> { new Mock<IUserValidator<User>>().Object };
        var passwordValidators = new List<IPasswordValidator<User>> { new Mock<IPasswordValidator<User>>().Object };
        var keyNormalizerMock = new Mock<ILookupNormalizer>();
        var errorsMock = new Mock<IdentityErrorDescriber>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<User>>>();

        var mockUserManager = new Mock<UserManager<User>>(
            userStoreMock.Object,
            optionsMock.Object,
            passwordHasherMock.Object,
            userValidators,
            passwordValidators,
            keyNormalizerMock.Object,
            errorsMock.Object,
            serviceProviderMock.Object,
            loggerMock.Object
        );

        return mockUserManager;
    }

    public void Dispose()
    {
        ContextFactory.Destroy(Context);
        GC.SuppressFinalize(this);
    }
    
}