using MediatR;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.User;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.UserRequests.CheckUserEmail;

public class CheckUserEmailHandler : IRequestHandler<CheckUserEmailCommand, UserEmailResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public CheckUserEmailHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<UserEmailResponse> Handle(CheckUserEmailCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty())
            throw new ValidationException();

        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is null)
            return new UserEmailResponse { NewUser = true, IsSuccessfully = true };

        if (user.EmailConfirmed)
            throw new BadRequestException("аккаунт с такой почтой зареган, иди нахуй");

        var code = _emailService.GenerateRandomCode();
        await _emailService.SendEmailAsync(request.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new UserEmailResponse { Confirmation = true, IsSuccessfully = true };
    }
}