using MassTransit;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Consumers;

public class ChatConsumer : IConsumer<SendMessageRequest>
{
    private readonly IDbContext _context;

    public ChatConsumer(IDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<SendMessageRequest> context)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == context.Message.UserId)
            ?? throw new NotFoundException(UserErrorMessage.UserNotFound);
        
        var chatHistory = await _context.ChatHistories.FirstOrDefaultAsync(x => x.Id == context.Message.ChatId)
            ?? throw new NotFoundException(ChatErrorMessage.ChatNotFound);
        
        var message = new ChatMessage
        {
            Message = context.Message.Message,
            Time = TimeOnly.FromDateTime(DateTime.Now),
            Date = DateOnly.FromDateTime(DateTime.Now),
            User = user,
            ChatHistory = chatHistory
        };

        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }
}