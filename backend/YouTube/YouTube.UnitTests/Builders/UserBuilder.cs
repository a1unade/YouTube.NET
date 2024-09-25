using YouTube.Domain.Entities;

namespace YouTube.UnitTests.Builders;

public class UserBuilder
{
    private static readonly User _user = new User();
    
    
    private readonly UserInfo _userInfo = new UserInfo
    {
        Name = "Ilya",
        Surname = "Vakatov",
        User = _user
    };

    private UserBuilder()
    {
    }

    /// <summary>
    /// Создать builder
    /// </summary>
    /// <returns></returns>
    public static UserBuilder CreateBuilder()
        => new();
    
    /// <summary>
    /// Установить имя
    /// </summary>
    /// <param name="username">Имя</param>
    public UserBuilder SetUsername(string username)
    {
        _user.UserName = username;
        return this;
    }
    
    /// <summary>
    /// Установить email
    /// </summary>
    /// <param name="email">Имя</param>
    public UserBuilder SetEmail(string email)
    {
        _user.Email = email;
        return this;
    }
    
    /// <summary>
    /// Установить дату рождения
    /// </summary>
    /// <param name="birthday">Дата рождения</param>
    public UserBuilder SetBirthday(DateOnly birthday)
    {
        _userInfo.BirthDate = birthday;
        return this;
    }

    public UserBuilder SetId(string id)
    {
        _user.Id = Guid.Parse(id);
        return this;
    }

    public UserBuilder SetUserInfo()
    {
        _userInfo.Id = Guid.NewGuid();
        _userInfo.BirthDate = new DateOnly(2004, 01, 09);
        _userInfo.Country = "Russia";
        _userInfo.Gender = "Male";
        _user.UserInfo = _userInfo;
        return this;
    }

    // /// <summary>
    // /// Установить Пароль
    // /// </summary>
    // /// <param name="password">Пароль</param>
    // /// <returns></returns>
    // public UserBuilder SetPassword(string password)
    // {
    //     _user.PasswordHash = _userManager.PasswordHasher.HashPassword(_user, password);
    //     return this;
    // }

    public User Build() => _user;
}