using Grpc.Core;
using YouTube.Proto;

namespace YouTube.Infrastructure.Helpers;

public interface IChatConnectionManager
{
    Task SubscribeAsync(string chatId, IServerStreamWriter<ChatMessageResponse> responseStream, CancellationToken token);
    Task PublishAsync(string chatId, ChatMessageResponse message);
}