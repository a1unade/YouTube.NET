using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.UserRequests.ChangePassword;
/// <summary>
/// Обработчик смены пароля (когда пользователь зарегистрирован)
/// </summary>
public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, BaseResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IEmailService _emailService;

    public ChangePasswordHandler(UserManager<Domain.Entities.User> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }
    
    public async Task<BaseResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.Password.IsNullOrEmpty() || request.Id == Guid.Empty)
            throw new ValidationException();

        var user = await _userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        await _userManager.ResetPasswordAsync(user, token, request.Password);

        await _emailService.SendEmailAsync(user.Email!, UserSuccessMessage.PasswordChanged,
            EmailSuccessMessage.EmailWarning); 
        
        return new BaseResponse { IsSuccessfully = true, Message = UserSuccessMessage.PasswordChanged };
    }
}