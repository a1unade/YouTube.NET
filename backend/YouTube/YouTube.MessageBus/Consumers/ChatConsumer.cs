using MassTransit;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.Interfaces;

namespace YouTube.MessageBus.Consumers;

public class ChatConsumer : IConsumer<SendMessageRequest>
{
    private readonly IChatService _chatService;
    
    public ChatConsumer(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task Consume(ConsumeContext<SendMessageRequest> context)
    {
        await _chatService.AddMessageAsync(context);
    }
}