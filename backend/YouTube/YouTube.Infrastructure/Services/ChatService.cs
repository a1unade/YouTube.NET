using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;
using YouTube.Infrastructure.Hubs;

namespace YouTube.Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly IDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IHubContext<SupportChatHub> _hubContext;

    
    public ChatService(IDbContext context, UserManager<User> userManager, IHubContext<SupportChatHub> hubContext)
    {
        _context = context;
        _userManager = userManager;
        _hubContext = hubContext;
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

    public async Task AddMessageAsync(ConsumeContext<SendMessageRequest> messageInfo)
    {
        var messageContext = messageInfo.Message;
        
        var user = await _context.Users
                       .Include(x => x.ChatHistory)
                       .FirstOrDefaultAsync(x => x.Id == messageContext.UserId)
                   ?? throw new NotFoundException(UserErrorMessage.UserNotFound);
        
        var message = new ChatMessage
        {
            Message = messageContext.Message,
            Time = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            User = user,
            ChatHistory = user.ChatHistory
        };

        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
        
        await _hubContext.Clients.Group(messageContext.ChatId.ToString())
            .SendAsync("ReceiveMessage", new
            {
                MessageId = message.Id,
                UserId = message.User,
                ChatId = message.ChatHistoryId,
                Message = message.Message,
                Date = message.Date,
                Time = message.Time,
            });
    }
    
    public async Task ReadMessagesAsync(List<Guid> messages) 
        => await _context.ChatMessages
            .Where(x => messages.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.
                SetProperty(r => r.IsRead, true));
}