using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;

namespace YouTube.Application.Features.Email.ForgotPasswordSendEmail;

public class ForgotPasswordSendEmailHandler : IRequestHandler<ForgotPasswordSendEmailCommand, BaseResponse>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public ForgotPasswordSendEmailHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager)
    {
        _emailService = emailService;
        _userManager = userManager;
    }
    public async Task<BaseResponse> Handle(ForgotPasswordSendEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty())
            throw new ValidationException();
        
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);

        string code = _emailService.GenerateRandomCode();

        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailChangePasswordCode, code));

        await _emailService.SendEmailAsync(user.Email!, EmailSuccessMessage.EmailChangePasswordCode, code);

        return new BaseResponse { IsSuccessfully = true, Message = EmailSuccessMessage.CheckYourEmail};
    }
}