namespace YouTube.Data.S3.Options;

public class MinioOptions
{
    /// <summary>
    /// Url
    /// </summary>
    public string EndPoint { get; set; } = default!;
    
    /// <summary>
    /// Логин
    /// </summary>
    public string AccessKey { get; set; } = default!;

    /// <summary>
    /// Секрет
    /// </summary>
    public string SecretKey { get; set; } = default!;
}