using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Messages.Error;
using YouTube.Application.Common.Messages.Success;
using YouTube.Application.Common.Responses.Auth;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;

namespace YouTube.Application.Features.Auth.Authorization;

public class AuthHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmailService _emailService;
    private readonly IDbContext _context;

    public AuthHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserRepository userRepository,
        IJwtGenerator jwtGenerator,
        IEmailService emailService,
        IDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
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

        var user = await _userRepository.FindByEmail(request.Email, cancellationToken);

        if (user is not null)
            throw new BadRequestException(AuthErrorMessages.EmailIsBusy);

        user = new User
        {
            UserName = request.Name + request.SurName,
            Email = request.Email,
            EmailConfirmed = false
        };
        
        var userInfo = new UserInfo
        {
            Name = request.Name,
            Surname = request.SurName,
            BirthDate = DateOnly.FromDateTime(request.DateOfBirth),
            Gender = request.Gender,
            Country = request.Country
        };

        user.UserInfo = userInfo;
        
        var channel = new Domain.Entities.Channel
        {
            Id = Guid.NewGuid(),
            Name = user.UserName,
            Description = null,
            CreateDate = DateOnly.FromDateTime(DateTime.Today),
            SubCount = 0,
            Country = request.Country,
            User = user
        };
        

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new IdentityException(errors, HttpStatusCode.BadRequest);
        }
        
        await _context.Channels.AddAsync(channel, cancellationToken);
        await _signInManager.SignInAsync(user, false);

        var code = _emailService.GenerateRandomCode();
        await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));
        await _emailService.SendEmailAsync(user.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

        return new AuthResponse
        {
            IsSuccessfully = true,
            Token = _jwtGenerator.GenerateToken(user),
            UserId = user.Id
        };
    }
}