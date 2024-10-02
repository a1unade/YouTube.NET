using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Email.CodeCheckForConfirmEmail;

public class CodeCheckForConfirmEmailHandler : IRequestHandler<CodeCheckForConfirmEmailCommand, BaseResponse>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserRepository _userRepository;

    public CodeCheckForConfirmEmailHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager,
        IUserRepository userRepository)
    {
        _emailService = emailService;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(CodeCheckForConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Code) || request.Id == Guid.Empty)
            throw new ValidationException();

        var user = await _userRepository.FindById(request.Id, cancellationToken);

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);

        var claims = await _userManager.GetClaimsAsync(user);

        if (!claims.Any())
            throw new BadRequestException(AnyErrorMessage.ClaimsIsEmpty);

        var claim = claims.FirstOrDefault(c =>
            c.Type == EmailSuccessMessage.EmailConfirmCodeString && c.Value == request.Code);

        if (claim == null)
            throw new BadRequestException(AnyErrorMessage.InvalidConfirmationCode);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _userManager.ConfirmEmailAsync(user, code);

        await _userManager.RemoveClaimAsync(user, claim);

        await _emailService.SendEmailAsync(user.Email!,
            EmailSuccessMessage.EmailThankYouMessage,
            EmailSuccessMessage.EmailConfirmCodeSuccess);

        return new BaseResponse { IsSuccessfully = true, Message = EmailSuccessMessage.EmailConfirmCodeSuccess};
    }
}