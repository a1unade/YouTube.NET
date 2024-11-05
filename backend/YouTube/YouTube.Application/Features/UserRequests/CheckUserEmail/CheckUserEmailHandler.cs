using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.User;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.UserRequests.CheckUserEmail;

public class CheckUserEmailHandler : IRequestHandler<CheckUserEmailCommand, UserEmailResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public CheckUserEmailHandler(IEmailService emailService, UserManager<User> userManager)
    {
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task<UserEmailResponse> Handle(CheckUserEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty())
            throw new ValidationException(UserErrorMessage.EmailNotCorrect);

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return new UserEmailResponse { NewUser = true, IsSuccessfully = true };

        if (user.EmailConfirmed)
            return new UserEmailResponse
            {
                IsSuccessfully = true,
                NewUser = false,
                Confirmation = true
            };
        
        var code = _emailService.GenerateRandomCode();
        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        await _emailService.SendEmailAsync(request.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new UserEmailResponse { IsSuccessfully = true };
    }
}