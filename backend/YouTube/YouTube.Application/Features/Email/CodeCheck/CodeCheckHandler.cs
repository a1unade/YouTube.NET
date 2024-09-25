using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Features.EmailFeatures.CodeCheck;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;

namespace YouTube.Application.Features.Email.CodeCheck;

public class CodeCheckHandler : IRequestHandler<CodeCheckCommand, BaseResponse>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;

    public CodeCheckHandler(IEmailService emailService, UserManager<Domain.Entities.User> userManager,
        IGenericRepository<Domain.Entities.User> userRepository)
    {
        _emailService = emailService;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(CodeCheckCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.Id))
            throw new ValidationException();

        var user = await _userRepository.GetById(Guid.Parse(request.Id), cancellationToken);

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

        return new BaseResponse { IsSuccessfully = true };
    }
}