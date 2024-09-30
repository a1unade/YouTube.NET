using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.User.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, BaseResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ChangePasswordHandler(UserManager<Domain.Entities.User> userManager,IUserRepository userRepository, IEmailService emailService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _emailService = emailService;
    }
    public async Task<BaseResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.Password.IsNullOrEmpty() || request.Email.IsNullOrEmpty())
            throw new ValidationException();

        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.Select(x => x.Description).ToString()!);

        await _emailService.SendEmailAsync(request.Email, UserSuccessMessage.PasswordChanged,
            EmailSuccessMessage.EmailWarning); 
        
        return new BaseResponse { IsSuccessfully = true, Message = UserSuccessMessage.PasswordChanged };
    }
}