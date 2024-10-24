using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Persistence.Repositories;

[ExcludeFromCodeCoverage]
public class ChatRepository : IChatRepository
{
    private readonly IDbContext _context;

    public ChatRepository(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<ChatHistory?> GetChatHistoryById(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ChatHistories
            .AsNoTracking()
            .Include(x => x.ChatMessages)
            .ThenInclude(x => x.File)
            .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
    }
}