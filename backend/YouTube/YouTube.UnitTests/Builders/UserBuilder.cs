using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.UnitTests.Builders;

public class UserBuilder
{
    private static readonly User User = new User();    
    
    private readonly UserInfo _userInfo = new UserInfo
    {
        Name = "Ilya",
        Surname = "Vakatov",
        UserId = User.Id,
    };

    private static readonly Channel Channel = new Channel
    {
        Name = "fafawfjaifijawf",
        CreateDate = DateOnly.FromDateTime(DateTime.Today),
        SubCount = 0,
        Country = "Russia"
    };
    
    private static readonly Video Video = new Video
    {
        Name = "Bogoeg",
        Country = "Russia",
        ReleaseDate = default,
        ChannelId = Channel.Id,
        Channel = Channel
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
    /// Установить имя
    /// </summary>
    /// <param name="name">имя</param>
    /// <returns></returns>
    public UserBuilder SetDisplayName(string name)
    {
        User.DisplayName = name;
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
    /// Задать канал полюзователю
    /// </summary>
    /// <returns></returns>
    public UserBuilder SetChannel()
    {
        User.Channels = new List<Channel>();
        User.Channels.Add(Channel);
        return this;
    }

    /// <summary>
    /// Задать видео каналу пользователя
    /// </summary>
    /// <returns></returns>
    public UserBuilder SetVideoAndFiles()
    {
        Video.VideoUrl = new File
        {
            Path = "FWAaf",
            FileName = "FWAwwwww",
            BucketName = "FAWFAf"
        };
        Video.PreviewImg = new File
        {
            Path = "fawfafw",
            FileName = "ffawfaff",
            BucketName = "fwfafwafawf"
        };
        Channel.Videos = new List<Video>();
        Channel.Videos.Add(Video);
        
        return this;
    }
    
    /// <summary>
    /// Билд юзера
    /// </summary>
    /// <returns>Юзер</returns>
    public User Build() => User;
}