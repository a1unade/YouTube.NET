using MassTransit;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;

namespace YouTube.Infrastructure.Services;

public class ChatService : IChatService
{
    private readonly IPublishEndpoint _bus;

    public ChatService(IPublishEndpoint bus)
    {
        _bus = bus;
    }
    
    public async Task SendMessageAsync(MessageInfo chatMessage)
    {
        await _bus.Publish(chatMessage);
    }
}