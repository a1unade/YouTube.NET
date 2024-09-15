using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public LoginHandler(UserManager<Domain.Entities.User> userManager, IJwtGenerator jwtGenerator, SignInManager<Domain.Entities.User> signInManager)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _signInManager = signInManager;
    }
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        
        // TODO (Валидация)

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new AuthResponse { Message = AuthErrorMessages.UserNotFound };

        var fl = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!fl)
            return new AuthResponse { Message = AuthErrorMessages.LoginWrongPassword };

        await _signInManager.SignInAsync(user, false);

        return new AuthResponse { IsSuccessfully = true, Token = _jwtGenerator.GenerateToken(user), UserId = user.Id };
    }
}