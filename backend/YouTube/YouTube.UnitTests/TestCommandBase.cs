using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;
using YouTube.UnitTests.Builders;

namespace YouTube.UnitTests;

public class TestCommandBase : IDisposable
{
    protected readonly ApplicationDbContext Context;
    protected Mock<IEmailService> EmailService { get; }
    protected Mock<IJwtGenerator> JwtGenerator { get; }
    protected Mock<IUserRepository> UserRepository { get; }
    protected Mock<UserManager<User>> UserManager { get; }
    protected Mock<SignInManager<User>> SignInManager { get; }
    
    public TestCommandBase()
    {
        Context = ContextFactory.Create();
        
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
        
        
        // Мокирование UserRepository
        UserRepository = new Mock<IUserRepository>();
        UserRepository.Setup(x => x.FindByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string email, CancellationToken _) =>
            {
                return Context.Users.FirstOrDefault(user => user.Email == email);
            });      
        
        UserRepository.Setup(x => x.FindById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken _) =>
            {
                return Context.Users.FirstOrDefault(x => x.Id == id);
            });

        
        
        // Мокирование UserManager
        UserManager = CreateMockUserManager();
        
        UserManager.Setup(x => x.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
        
        UserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        UserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
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