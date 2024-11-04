using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Chats.GetChatCollection;

public class GetChatCollectionQueryHandler : IRequestHandler<GetChatCollectionQuery, ChatCardResponse>
{
     private readonly IS3Service _s3Service;
    private readonly IChatRepository _repository;

    public GetChatCollectionQueryHandler(IS3Service s3Service, IChatRepository repository)
    {
        _s3Service = s3Service;
        _repository = repository;
    }

    public async Task<ChatCardResponse> Handle(GetChatCollectionQuery request, CancellationToken cancellationToken)
    {
        if (request.Page <= 0 || request.Size <= 0)
            throw new ValidationException();

        var histories = await _repository.GetChatHistoryPagination(request.Page, request.Size, cancellationToken);

        if (histories.Count == 0)
            return new ChatCardResponse { IsSuccessfully = true, Message = "Сообщений нет" };

        var chatsDto = new List<ChatCardDto>();

        foreach (var history in histories)
        {
            var messages = history.ChatMessages.OrderByDescending(x => x.Date).FirstOrDefault();
            var chatCardDto = new ChatCardDto();

            if (messages is not null)
            {
                chatCardDto.LastMessage = new ChatMessageDto
                {
                    SenderId = messages.UserId,
                    MessageId = messages.Id,
                    Message = messages.Message,
                    Time = messages.Time,
                    IsRead = messages.IsRead,
                    Date = messages.Date
                };
            }
            chatCardDto.ChatId = history.Id;
            chatCardDto.UserName = history.User.DisplayName;
            chatCardDto.AvatarUrl = (history.User.AvatarUrl is null
                ? null
                : await _s3Service.GetFileUrlAsync(history.User.AvatarUrl.BucketName, history.User.AvatarUrl.FileName,
                    cancellationToken))!;

            chatsDto.Add(chatCardDto);
        }

        return new ChatCardResponse
        {
            IsSuccessfully = true,
            ChatCardDtos = chatsDto
        };
    }
}