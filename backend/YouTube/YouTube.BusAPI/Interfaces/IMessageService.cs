using MassTransit;
using YouTube.Application.Common.Requests.Chats;
using YouTube.Application.DTOs.Chat;

namespace YouTube.BusAPI.Interfaces;

public interface IMessageService
{
    Task AddMessageAsync(ConsumeContext<MessageRequest> messageInfo);
}