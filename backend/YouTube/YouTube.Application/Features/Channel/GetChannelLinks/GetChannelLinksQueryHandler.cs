using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Channel;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Channel.GetChannelLinks;

public class GetChannelLinksQueryHandler : IRequestHandler<GetChannelLinksQuery, ChannelLinksResponse>
{
    private readonly IChannelRepository _repository;

    public GetChannelLinksQueryHandler(IChannelRepository repository)
    {
        _repository = repository;
    }
    public async Task<ChannelLinksResponse> Handle(GetChannelLinksQuery request, CancellationToken cancellationToken)
    {
        if (request is null || request.Id == Guid.Empty)
            throw new ValidationException(AnyErrorMessage.IdIsNotCorrect);

        var channel = await _repository.GetChannelWithLinks(request.Id, cancellationToken);

        if (channel is null)
            throw new NotFoundException();

        return new ChannelLinksResponse
        {
            IsSuccessfully = true,
            Links = channel.Links.Select(x => x.Reference).ToList(),
            Videos = channel.Videos.Count,
            Subscribers = channel.Subscribers.Count,
            Views = channel.Videos.Sum(x => x.ViewCount),
            Country = channel.Country ?? "Нет страны"
        };
    }
}