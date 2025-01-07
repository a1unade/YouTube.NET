using YouTube.Domain.Entities;
using File = YouTube.Domain.Entities.File;

namespace YouTube.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    private static readonly IDictionary<Type, string> EntityException = new Dictionary<Type, string>
    {
        [typeof(File)] = "Файл не найден",
        [typeof(Channel)] = "Канал не найден",
        [typeof(User)] = "Пользователь не найден",
        [typeof(UserInfo)] = "Информация о пользователе не найдена",
        [typeof(Video)] = "Видео не найдено",
        [typeof(Playlist)] = "Плейлист не найден",
        [typeof(ChatMessage)] = "Сообщение не найдено",
        [typeof(ChatHistory)] = "История чата не найдена",
        [typeof(Category)] = "Категория не найдена"
    };

    public NotFoundException(Guid id)
        : base($"Сущность с идентификатором {id} не найдена.")
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(Type entityType)
        : base(ExceptionEntity(entityType))
    {
    }

    public NotFoundException()
        : base("Сущность не найдена")
    {
    }

    private static string ExceptionEntity(Type entityType) =>
        EntityException.TryGetValue(entityType, out var text) ? text : $"{entityType.FullName} не найдена.";
}