using YouTube.Domain.Entities;

namespace YouTube.UnitTests.Builders;

public class UserBuilder
{
    private static readonly User User = new User();    
    
    private readonly UserInfo _userInfo = new UserInfo
    {
        Name = "Ilya",
        Surname = "Vakatov",
        UserId = User.Id,
        User = User
    };
    
    private UserBuilder()
    {
        
    }

    /// <summary>
    /// Создать builder
    /// </summary>
    /// <returns>UserBuilder</returns>
    public static UserBuilder CreateBuilder()
        => new();
    
    /// <summary>
    /// Установить имя
    /// </summary>
    /// <param name="username">Имя</param>
    /// <returns>UserBuilder</returns>

    public UserBuilder SetUsername(string username)
    {
        User.UserName = username;
        return this;
    }
    
    /// <summary>
    /// Установить email
    /// </summary>
    /// <param name="email">Имя</param>
    /// <returns>UserBuilder</returns>

    public UserBuilder SetEmail(string email)
    {
        User.Email = email;
        return this;
    }
    
    /// <summary>
    /// Установить дату рождения
    /// </summary>
    /// <param name="birthday">Дата рождения</param>
    /// <returns>UserBuilder</returns>

    public UserBuilder SetBirthday(DateOnly birthday)
    {
        _userInfo.BirthDate = birthday;
        return this;
    }

    /// <summary>
    /// Установить Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>UserBuilder</returns>
    public UserBuilder SetId(string id)
    {
        User.Id = Guid.Parse(id);
        return this;
    }

    /// <summary>
    /// Задать доп информацию
    /// </summary>
    /// <returns>UserBuilder</returns>
    public UserBuilder SetUserInfo()
    {
        _userInfo.Id = Guid.NewGuid();
        _userInfo.BirthDate = new DateOnly(2004, 01, 09);
        _userInfo.Country = "Russia";
        _userInfo.Gender = "Male";
        User.PasswordHash = "Ilya1337";
        User.UserInfo = _userInfo;
        return this;
    }
    
    /// <summary>
    /// Билд юзера
    /// </summary>
    /// <returns>Юзер</returns>
    public User Build() => User;
}