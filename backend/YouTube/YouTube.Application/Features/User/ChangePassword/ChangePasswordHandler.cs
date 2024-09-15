using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.User.ChangePassword;

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
        //TODO Validation

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new BaseResponse { Message = UserErrorMessage.UserNotFound };
        
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (result.Succeeded)
            return new BaseResponse { IsSuccessfully = true, Message = UserSuccessMessage.PasswordChanged };

        await _emailService.SendEmailAsync(request.Email, UserSuccessMessage.PasswordChanged,
            EmailSuccessMessage.EmailWarning); 
        
        return new BaseResponse
        {
            Error = result.Errors
                .Select(x => x.Description)
                .ToList()
        };
    }
}