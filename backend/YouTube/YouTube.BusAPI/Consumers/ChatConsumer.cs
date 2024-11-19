using MassTransit;
using YouTube.Application.DTOs.Chat;
using YouTube.BusAPI.Interfaces;

namespace YouTube.BusAPI.Consumers;

public class ChatConsumer : IConsumer<MessageRequest>
{
    private readonly IMessageService _messageService;
    
    public ChatConsumer(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public async Task Consume(ConsumeContext<MessageRequest> context)
    {
        await _messageService.AddMessageAsync(context);
    }
}