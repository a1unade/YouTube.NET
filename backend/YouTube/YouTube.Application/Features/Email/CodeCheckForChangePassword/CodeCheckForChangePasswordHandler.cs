using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.User;

namespace YouTube.Application.Features.Email.CodeCheckForChangePassword;

public class CodeCheckForChangePasswordHandler : IRequestHandler<CodeCheckForChangePasswordCommand, UserIdResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;

    public CodeCheckForChangePasswordHandler(UserManager<Domain.Entities.User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<UserIdResponse> Handle(CodeCheckForChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty() || request.Code.IsNullOrEmpty())
            throw new ValidationException();

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            throw new NotFoundException(UserErrorMessage.UserNotFound);

        var claims = await _userManager.GetClaimsAsync(user);
        
        if(!claims.Any())
            throw new BadRequestException(AnyErrorMessage.ClaimsIsEmpty);
        
        var claim = claims.FirstOrDefault(c =>
            c.Type == EmailSuccessMessage.EmailChangePasswordCode && c.Value == request.Code);
        
        if (claim == null)
            throw new BadRequestException(AnyErrorMessage.InvalidConfirmationCode);

        return new UserIdResponse { IsSuccessfully = true, Id = user.Id };
    }
}