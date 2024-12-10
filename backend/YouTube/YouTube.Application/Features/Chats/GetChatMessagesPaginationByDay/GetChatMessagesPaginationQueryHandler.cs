using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Chats.GetChatMessagesPaginationByDay;

public class GetChatMessagesPaginationQueryHandler : IRequestHandler<GetChatMessagesPaginationQuery, ChatHistoryResponse>
{
    private readonly IChatRepository _chatRepository;

    public GetChatMessagesPaginationQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<ChatHistoryResponse> Handle(GetChatMessagesPaginationQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Page <= 0 || request.ChatId == Guid.Empty)
            throw new ValidationException();

        var chatHistory =
            await _chatRepository.GetChatMessagesPagination(request.ChatId, request.Page, cancellationToken);

        if (chatHistory.Count == 0)
            return new ChatHistoryResponse { IsSuccessfully = true, Message = " Сообщений нет" };

        var messagesDto = new List<ChatMessageDto>();

        foreach (var messages in chatHistory)
        {
            messagesDto.Add(new ChatMessageDto
            {
                SenderId = messages.UserId,
                MessageId = messages.Id,
                Message = messages.Message,
                Time = messages.Time,
                Date = messages.Date,
                IsRead = messages.IsRead,
                FileId = messages.FileId
            });
        }

        return new ChatHistoryResponse
        {
            IsSuccessfully = true,
            ChatMessages = messagesDto,
            PageCount = await _chatRepository.GetUniqueDates(request.ChatId, cancellationToken)
        };
    }
}