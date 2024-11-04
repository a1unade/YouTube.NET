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
            .Include(x => x.User)
            .ThenInclude(x => x.AvatarUrl)
            .Include(x => x.ChatMessages)
            .ThenInclude(x => x.File)
            .FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
    }
    public async Task<List<ChatHistory>> GetChatHistoryPagination(int page, int size, CancellationToken cancellationToken)
    {
        return await _context.ChatHistories
            .AsNoTracking()
            .Include(x => x.ChatMessages)
            .ThenInclude(x => x.File)
            .Include(x => x.User)
            .ThenInclude(x => x.AvatarUrl)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ChatMessage>> GetChatMessagesPagination(Guid chatHistoryId, int page, CancellationToken cancellationToken)
    {
        var uniqueDates = await _context.ChatHistories
            .AsNoTracking()
            .Where(ch => ch.Id == chatHistoryId)
            .SelectMany(ch => ch.ChatMessages)
            .Select(cm => cm.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .ToListAsync(cancellationToken);

        var targetDate = uniqueDates.Skip(page - 1).FirstOrDefault();
        
        if (targetDate == default)
            return new List<ChatMessage>();
        
        var paginatedMessages = await _context.ChatHistories
            .AsNoTracking()
            .Where(ch => ch.Id == chatHistoryId)
            .SelectMany(ch => ch.ChatMessages)
            .Where(cm => cm.Date == targetDate)
            .Include(cm => cm.File)
            .OrderByDescending(x => x.Time)
            .ToListAsync(cancellationToken);

        return paginatedMessages;
    }
}