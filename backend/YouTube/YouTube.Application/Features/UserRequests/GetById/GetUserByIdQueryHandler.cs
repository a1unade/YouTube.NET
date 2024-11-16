using MediatR;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Responses.User;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.UserRequests.GetById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserInfoResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserInfoResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new ValidationException();

        var user = await _userRepository.FindById(request.Id, cancellationToken);

        if (user == null || user.UserInfo == null)
            throw new NotFoundException(typeof(User));
        
        return new UserInfoResponse
        {
            IsSuccessfully = true,
            Name = user.UserInfo.Name,
            SurName = user.UserInfo.Surname!,
            Email = user.Email!,
            UserName = user.DisplayName,
            IsPremium = user.Subscriptions != null
        };
    }
}