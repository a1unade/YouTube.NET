using MediatR;
using Microsoft.EntityFrameworkCore;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Chats.PatchReadMessages;

public class PatchReadMessagesCommandHandler : IRequestHandler<PatchReadMessagesCommand, BaseResponse>
{
    private readonly IDbContext _context;

    public PatchReadMessagesCommandHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async Task<BaseResponse> Handle(PatchReadMessagesCommand request, CancellationToken cancellationToken)
    {
        if (request.MessagesId.Count == 0)
            throw new ValidationException();

        int affectedRows = await _context.ChatMessages
            .Where(x => request.MessagesId.Contains(x.Id))
            .ExecuteUpdateAsync(x => x.
                SetProperty(r => r.IsRead, true), cancellationToken);

        if (affectedRows == 0)
            throw new NotFoundException();
        
        await _context.SaveChangesAsync(cancellationToken);

        return new BaseResponse
        {
            IsSuccessfully = true,
            Message = null
        };
    }
}