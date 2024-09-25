using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.Auth.Authorization;

public class AuthHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly SignInManager<Domain.Entities.User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmailService _emailService;
    private readonly IDbContext _context;

    public AuthHandler(UserManager<Domain.Entities.User> userManager, SignInManager<Domain.Entities.User> signInManager,
        IJwtGenerator jwtGenerator,
        IEmailService emailService,
        IDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _emailService = emailService;
        _context = context;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty() || request.Password.IsNullOrEmpty() || 
            request.Gender.IsNullOrEmpty() || request.SurName.IsNullOrEmpty() ||
            request.Name.IsNullOrEmpty())
            throw new ValidationException();

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
            throw new NotFoundException(AuthErrorMessages.EmailIsBusy);

        var userInfo = new UserInfo
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.SurName,
            BirthDate = request.DateOfBirth,
            Gender = request.Gender,
            Country = "Empty"
        };

        user = new Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            UserName = request.Name + request.SurName,
            Email = request.Email,
            EmailConfirmed = false,
            UserInfoId = userInfo.Id,
            UserInfo = userInfo
        };

        var channel = new Channel
        {
            Id = Guid.NewGuid(),
            Name = user.UserName + user.Id.ToString().Substring(0, 5),
            Description = null,
            CreateDate = DateOnly.FromDateTime(DateTime.Today),
            SubCount = 0,
            Country = userInfo.Country,
            UserId = user.Id,
            User = user
        };
        await _context.Channels.AddAsync(channel, cancellationToken);

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        // TODO ТУТ надо протестировать 
        if (!result.Succeeded)
            throw new NotFoundException(result.Errors.Select(x => x.Description).ToString()!);

        await _signInManager.SignInAsync(user, false);
        
        await _emailService.SendEmailAsync(user.Email, EmailSuccessMessage.EmailSuccessRegistrationMessage,
            EmailSuccessMessage.EmailThankYouMessage);

        return new AuthResponse
        {
            IsSuccessfully = true,
            Token = _jwtGenerator.GenerateToken(user),
            UserId = user.Id
        };
    }
}