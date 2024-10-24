using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces.Repositories;

public interface IChatRepository
{
    Task<ChatHistory?> GetChatHistoryById(Guid id, CancellationToken cancellationToken);
}