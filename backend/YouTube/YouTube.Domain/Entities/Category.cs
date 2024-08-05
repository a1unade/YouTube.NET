using YouTube.Domain.Common;

namespace YouTube.Domain.Entities;

public class Category : BaseEntity
{
    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
}