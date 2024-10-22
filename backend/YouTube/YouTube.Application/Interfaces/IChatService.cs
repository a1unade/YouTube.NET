
using YouTube.Application.DTOs.Chat;
using YouTube.Domain.Entities;

namespace YouTube.Application.Interfaces;

public interface IChatService
{
    Task SendMessageAsync(MessageInfo chatMessage);
}