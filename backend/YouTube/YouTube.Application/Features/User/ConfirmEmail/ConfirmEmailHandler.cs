using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.User.ConfirmEmail;

public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, BaseResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    
    public  ConfirmEmailHandler(UserManager<Domain.Entities.User> userManager, IUserRepository userRepository, IEmailService emailService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<BaseResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty() || request.Id.IsNullOrEmpty())
            throw new ValidationException();
        
        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);

        string code = _emailService.GenerateRandomCode();

        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
            
        await _emailService.SendEmailAsync(request.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new BaseResponse { IsSuccessfully = true };
    }
}