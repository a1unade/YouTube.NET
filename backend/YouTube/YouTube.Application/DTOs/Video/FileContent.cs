namespace YouTube.Application.DTOs.Video;

public class FileContent
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="content">Бинарные данные</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип контента</param>
    /// <param name="bucket">Название бакета</param>
    public FileContent(
        Stream content,
        string fileName,
        string contentType,
        string bucket)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType;
        Bucket = bucket;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public FileContent()
    {
    }

    /// <summary>
    /// Бинарные данные файла
    /// </summary>
    public Stream Content { get; set; } 
    
    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Размер в байтах
    /// </summary>
    public long Lenght { get; set; }

    /// <summary>
    /// Название бакета
    /// </summary>
    public string Bucket { get; set; } = default!;

    /// <summary>
    /// Массив Байтов
    /// </summary>
    public byte[]? Bytes { get; set; } = default!;
}