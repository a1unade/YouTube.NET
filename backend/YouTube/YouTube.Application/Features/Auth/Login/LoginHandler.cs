using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;

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

        if (request.Password.IsNullOrEmpty() || request.Email.IsNullOrEmpty())
            throw new ValidationException("Пароль или почта не валидны");
        
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(AuthErrorMessages.UserNotFound);

        var fl = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!fl)
            throw new ValidationException(AuthErrorMessages.LoginWrongPassword);

        await _signInManager.SignInAsync(user, false);

        return new AuthResponse { IsSuccessfully = true, Token = _jwtGenerator.GenerateToken(user), UserId = user.Id };
    }
}