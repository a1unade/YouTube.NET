using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly SignInManager<Domain.Entities.User> _signInManager;

    public LoginHandler(UserManager<Domain.Entities.User> userManager,IUserRepository userRepository, IJwtGenerator jwtGenerator, SignInManager<Domain.Entities.User> signInManager)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _jwtGenerator = jwtGenerator;
        _signInManager = signInManager;
    }
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        if (request.Password.IsNullOrEmpty() || request.Email.IsNullOrEmpty())
            throw new ValidationException("Пароль или почта не валидны");
        
        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is null)
            throw new NotFoundException(AuthErrorMessages.UserNotFound);

        var fl = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!fl)
            throw new BadRequestException(AuthErrorMessages.LoginWrongPassword);

        await _signInManager.SignInAsync(user, false);

        var jwtToken = await _jwtGenerator.GenerateToken(user); 

        return new AuthResponse { IsSuccessfully = true, Token = jwtToken, UserId = user.Id };
    }
}