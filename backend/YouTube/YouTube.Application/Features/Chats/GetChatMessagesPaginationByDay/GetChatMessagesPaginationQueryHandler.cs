using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Chats.GetChatMessagesPaginationByDay;

public class GetChatMessagesPaginationQueryHandler : IRequestHandler<GetChatMessagesPaginationQuery, ChatHistoryResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IS3Service _s3Service;

    public GetChatMessagesPaginationQueryHandler(IChatRepository chatRepository, IS3Service s3Service)
    {
        _chatRepository = chatRepository;
        _s3Service = s3Service;
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
                Message = messages.Message,
                Time = messages.Timestamp,
                IsRead = messages.IsRead,
                ContentType = messages.File?.ContentType,
                FileUrl = messages.File is null
                    ? null
                    : await _s3Service.GetFileUrlAsync(messages.File.BucketName, messages.File.FileName, cancellationToken),
            });
        }

        return new ChatHistoryResponse
        {
            IsSuccessfully = true,
            ChatMessages = messagesDto,
        };
    }
}