using MassTransit;
using YouTube.Application.Common.Requests.Chats;

namespace YouTube.BusAPI.Interfaces;

public interface IMessageService
{
    Task AddMessageAsync(ConsumeContext<SendMessageRequest> messageInfo);
}