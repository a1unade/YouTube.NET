using YouTube.Domain.Common.Interfaces;

namespace YouTube.Domain.Entities;

public class Avatar
{
    /// <summary>
    /// ID аватарки
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Путь к статическому файлу
    /// </summary>
    public string Path { get; set; }
}