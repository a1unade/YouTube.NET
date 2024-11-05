using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly IDbContext _context;
    private readonly UserManager<User> _userManager;
    
    public ChatService(IDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public async Task<Guid> CreateChatAsync(Guid userId)
    {
        var user = await _context.Users
                       .Include(u => u.ChatHistory)
                       .FirstOrDefaultAsync(u => u.Id == userId) 
                   ?? throw new NotFoundException(UserErrorMessage.UserNotFound);

        if (!await _userManager.IsInRoleAsync(user, "User"))
            return Guid.Empty;

        if (user.ChatHistory != null!)
            return user.ChatHistory.Id;

        var chatHistory = new ChatHistory
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            UserId = user.Id,
            ChatMessages = new List<ChatMessage>()
        };

        _context.ChatHistories.Add(chatHistory);
        await _context.SaveChangesAsync();
        return chatHistory.Id;
    }

    public async Task ReadMessagesAsync(List<Guid> messages)
    {
        await _context.ChatMessages
            .Where(x => messages.Contains(x.Id))
            .ExecuteUpdateAsync(x => x
                .SetProperty(r => r.IsRead, true));
    }
}