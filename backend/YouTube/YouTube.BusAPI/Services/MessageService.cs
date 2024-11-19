using MassTransit;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.BusAPI.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.BusAPI.Services;

public class MessageService : IMessageService
{
    private readonly IDbContext _context;
    
    public MessageService(IDbContext context)
    {
        _context = context;
    }
    
    public async Task AddMessageAsync(ConsumeContext<MessageRequest> messageInfo)
    {
        var messageContext = messageInfo.Message;
        
        var user = await _context.Users
                       .FirstOrDefaultAsync(x => x.Id == messageContext.UserId)
                   ?? throw new NotFoundException(UserErrorMessage.UserNotFound);

        var chat = await _context.ChatHistories
                       .FirstOrDefaultAsync(x => x.Id == messageContext.ChatId)
                   ?? throw new NotFoundException(ChatErrorMessage.ChatNotFound);
        
        var message = new ChatMessage
        {
            Id = messageContext.MessageId,
            Message = messageContext.Message,
            Time = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            User = user,
            ChatHistory = chat
        };

        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }
}