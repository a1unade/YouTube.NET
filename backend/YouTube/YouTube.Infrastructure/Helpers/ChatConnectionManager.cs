using System.Text.Json;
using Grpc.Core;
using StackExchange.Redis;
using YouTube.Proto;

namespace YouTube.Infrastructure.Helpers;

public class ChatConnectionManager : IChatConnectionManager
{
    private readonly ISubscriber _subscriber;

    public ChatConnectionManager(IConnectionMultiplexer redis)
    {
        _subscriber = redis.GetSubscriber();
    }
    public async Task SubscribeAsync(string chatId, IServerStreamWriter<ChatMessageResponse> responseStream, CancellationToken token)
    {
        var channel = new RedisChannel($"chat:{chatId}", RedisChannel.PatternMode.Literal);

        await _subscriber.SubscribeAsync(channel, async void (_, message) =>
        {
            try
            {
                var chatMessage = JsonSerializer.Deserialize<ChatMessageResponse>(message!);
                if (chatMessage != null && !token.IsCancellationRequested)
                {
                    await responseStream.WriteAsync(chatMessage, token);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error while trying to send chat message");
            }
        });
        
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(500);
        }

        await _subscriber.UnsubscribeAsync(channel);
    }

    public async Task PublishAsync(string chatId, ChatMessageResponse message)
    {
        var json = JsonSerializer.Serialize(message);
        await _subscriber.PublishAsync($"chat:{chatId}", json);
    }
}