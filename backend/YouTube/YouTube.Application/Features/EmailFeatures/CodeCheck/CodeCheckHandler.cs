using MediatR;
using Microsoft.AspNetCore.Identity;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.EmailFeatures.CodeCheck;

public class CodeCheckHandler : IRequestHandler<CodeCheckCommand, BaseResponse>
{
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;
    private readonly IGenericRepository<User> _userRepository;


    public CodeCheckHandler(IEmailService emailService, UserManager<User> userManager,
        IGenericRepository<User> userRepository)
    {
        _emailService = emailService;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<BaseResponse> Handle(CodeCheckCommand request, CancellationToken cancellationToken)
    {
        if (request.Code is null || request.Id is null || request is null)
            return new BaseResponse { Message = AnyErrorMessage.RequestIsEmpty };

        var user = await _userRepository.GetById(Guid.Parse(request.Id), cancellationToken);

        if (user is null)
            return new BaseResponse { Message = UserErrorMessage.UserNotFound };

        var claims = await _userManager.GetClaimsAsync(user);

        if (claims.Count == 0 || claims is null)
            return new BaseResponse { Message = AnyErrorMessage.ClaimsIsEmpty };

        var claim = claims.FirstOrDefault(c =>
            c.Type == EmailSuccessMessage.EmailConfirmCodeString && c.Value == request.Code);

        if (claim == null)
            return new BaseResponse { Message = AnyErrorMessage.InvalidConfirmationCode };

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _userManager.ConfirmEmailAsync(user, code);

        await _userManager.RemoveClaimAsync(user, claim);

        await _emailService.SendEmailAsync(user.Email!, EmailSuccessMessage.EmailThankYouMessage,
            EmailSuccessMessage.EmailConfirmCodeSuccess);

        return new BaseResponse { IsSuccessfully = true };
    }
}