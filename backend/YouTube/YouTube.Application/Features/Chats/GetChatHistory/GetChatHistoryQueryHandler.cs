using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Chats;
using YouTube.Application.DTOs.Chat;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Chats.GetChatHistory;

public class GetChatHistoryQueryHandler : IRequestHandler<GetChatHistoryQuery, ChatHistoryResponse>
{
    private readonly IS3Service _s3Service;
    private readonly IChatRepository _repository;

    public GetChatHistoryQueryHandler(IS3Service s3Service, IChatRepository repository)
    {
        _s3Service = s3Service;
        _repository = repository;
    }
    
    public async Task<ChatHistoryResponse> Handle(GetChatHistoryQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException(AnyErrorMessage.IdIsNotCorrect);

        var chatHistory = await _repository.GetChatHistoryById(request.Id, cancellationToken);
        
        if (chatHistory is null)
            return new ChatHistoryResponse { IsSuccessfully = true, Message = "Истории нет" };

        var messagesDto = new List<ChatMessageDto>();
        
        foreach (var messages in chatHistory.ChatMessages)
        {
            messagesDto.Add(new ChatMessageDto
            {
                Time = messages.Timestamp,
                Message = messages.Message,
                FileUrl = messages.File is null 
                    ? null
                    : await _s3Service.GetFileUrlAsync(messages.File.BucketName, messages.File.FileName, cancellationToken)
            });
        }

        return new ChatHistoryResponse { IsSuccessfully = true, ChatMessages = messagesDto };
    }
}