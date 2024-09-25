using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
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
    protected Mock<IUserRepository> UserRepository { get; }
    protected Mock<UserManager<User>> UserManager { get; }

    public TestCommandBase()
    {
        Context = ContextFactory.Create();

        // Мокирование EmailService
        EmailService = new Mock<IEmailService>();
        EmailService.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.CompletedTask);
        EmailService.Setup(x => x.GenerateRandomCode())
            .Returns("123456");

        // Мокирование UserRepository
        UserRepository = new Mock<IUserRepository>();
        UserRepository.Setup(x => x.GetUserByEmail("bulatfri18@gmail.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User());
        UserRepository.Setup(x =>
                x.GetById(Guid.Parse("53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User());

        // Мокирование UserManager с необходимыми зависимостями
        UserManager = CreateMockUserManager();
        
        UserManager.Setup(x => x.AddClaimAsync(It.IsAny<User>(), It.IsAny<Claim>()))
            .ReturnsAsync(IdentityResult.Success);
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