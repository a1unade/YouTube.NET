using MassTransit;
using YouTube.Application.Common.Requests.Chats;
using YouTube.BusAPI.Interfaces;

namespace YouTube.BusAPI.Consumers;

public class ChatConsumer : IConsumer<SendMessageRequest>
{
    private readonly IMessageService _messageService;
    
    public ChatConsumer(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    public async Task Consume(ConsumeContext<SendMessageRequest> context)
    {
        await _messageService.AddMessageAsync(context);
    }
}