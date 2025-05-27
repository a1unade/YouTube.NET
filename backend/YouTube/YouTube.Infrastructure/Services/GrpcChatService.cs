using Grpc.Core;
using YouTube.Application.Interfaces;
using YouTube.Infrastructure.Helpers;
using YouTube.Proto;

namespace YouTube.Infrastructure.Services;

public class GrpcChatService : Proto.ChatService.ChatServiceBase
{
    private readonly IChatService _service;
    private readonly IChatConnectionManager _chatConnectionManager;


    public GrpcChatService(
        IChatService service,
        IChatConnectionManager chatConnectionManager
    )
    {
        _service = service;
        _chatConnectionManager = chatConnectionManager;
    }

    public override async Task<JoinChatResponse> JoinChat(JoinChatRequest request, ServerCallContext context)
    {
        try
        {
            if (string.IsNullOrEmpty(request.UserId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "user_id is null or empty"));

            Guid chatId;
            if (request.ChatId == null || string.IsNullOrEmpty(request.ChatId))
            {
                chatId = await _service.CreateChatAsync(Guid.Parse(request.UserId));
            }
            else
            {
                chatId = Guid.Parse(request.ChatId!);
            }

            return new JoinChatResponse
            {
                UserId = request.UserId,
                ChatId = chatId.ToString()
            };
        }
        catch (RpcException exception)
        {
            Console.WriteLine(exception.Status.ToString());
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StatusCode);
            Console.WriteLine(exception.Message, exception.StatusCode);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
    {
        try
        {
            if (string.IsNullOrEmpty(request.ChatId) || string.IsNullOrEmpty(request.UserId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "chat_id or user_id is null or empty"));

            var messageId = await _service.SendMessageAsync(new Application.Common.Requests.Chats.SendMessageRequest
            {
                ChatId = Guid.Parse(request.ChatId),
                UserId = Guid.Parse(request.UserId),
                Message = request.Message
            });
        
            await _chatConnectionManager.PublishAsync(request.ChatId, new ChatMessageResponse
            {
                MessageId = messageId.ToString(),
                UserId = request.UserId,
                Message = request.Message,
                IsRead = false,
                Date = DateOnly.FromDateTime(DateTime.UtcNow).ToString("yyyy-MM-dd"),
                Time = TimeOnly.FromDateTime(DateTime.UtcNow).ToString("HH:mm:ss")
            });
        
            return new SendMessageResponse { Success = true };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override async Task MessageStream(
        JoinChatRequest request,
        IServerStreamWriter<ChatMessageResponse> responseStream,
        ServerCallContext context
    )
    {
        if (string.IsNullOrEmpty(request.ChatId) || string.IsNullOrEmpty(request.UserId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "chat_id or user_id is null or empty"));

        await _chatConnectionManager.SubscribeAsync(request.ChatId, responseStream, context.CancellationToken);
    }
}