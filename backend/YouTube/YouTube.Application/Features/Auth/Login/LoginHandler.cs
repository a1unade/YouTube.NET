using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginHandler(UserManager<User> userManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
    }
    public Task<BaseResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        
        // TODO (Валидация)
        throw new NotImplementedException();
    }
}