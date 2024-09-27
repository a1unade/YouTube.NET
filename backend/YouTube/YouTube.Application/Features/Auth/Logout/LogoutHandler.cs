using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Auth.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand, BaseResponse>
{
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IUserRepository _repository;

    public LogoutHandler(SignInManager<Domain.Entities.User> signInManager, IUserRepository repository)
    {
        _signInManager = signInManager;
        _repository = repository;
    }
    public async Task<BaseResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId.IsNullOrEmpty())
            throw new ValidationException();
        
        var user = await _repository.FindById(Guid.Parse(request.UserId), cancellationToken);

        if (user is null)
            return new BaseResponse { Message = UserErrorMessage.UserNotFound };

        await _signInManager.SignOutAsync();
        
        return new BaseResponse { IsSuccessfully = true };
    }
}