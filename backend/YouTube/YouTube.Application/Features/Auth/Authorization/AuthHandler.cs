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
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;
    private readonly IDbContext _context;
    private readonly IPlaylistService _playlistService;

    public AuthHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserRepository userRepository,
        IJwtService jwtService,
        IEmailService emailService,
        IDbContext context,
        IPlaylistService playlistService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _jwtService = jwtService;
        _emailService = emailService;
        _context = context;
        _playlistService = playlistService;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        if (request.Email.IsNullOrEmpty() || request.Password.IsNullOrEmpty() ||
            request.Gender.IsNullOrEmpty() || request.SurName.IsNullOrEmpty() ||
            request.Name.IsNullOrEmpty())
        {
            throw new ValidationException();
        }

        // Проверка существования пользователя
        var existingUser = await _userRepository.FindByEmail(request.Email, cancellationToken);
        if (existingUser is not null)
            throw new BadRequestException(AuthErrorMessages.EmailIsBusy);

        // Начинаем транзакцию
        await using var transaction = await _context.BeginTransactionAsync(cancellationToken);

        try
        {
            // Создаем пользователя (без указания ID, чтобы Identity сам его сгенерировал)
            var user = new User
            {
                DisplayName = $"{request.Name} {request.SurName}",
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = false
            };

            // Создаем пользователя в Identity
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new IdentityException(errors, HttpStatusCode.BadRequest);
            }

            // Добавляем роль
            await _userManager.AddToRoleAsync(user, "User");

            // Создаем UserInfo
            user.UserInfo = new UserInfo
            {
                Name = request.Name,
                Surname = request.SurName,
                BirthDate = DateOnly.FromDateTime(request.DateOfBirth),
                Gender = request.Gender,
                Country = request.Country,
                UserId = user.Id // Явно устанавливаем связь
            };

            // Создаем канал
            var channel = new Domain.Entities.Channel
            {
                Id = Guid.NewGuid(),
                Name = user.DisplayName,
                Description = null,
                CreateDate = DateOnly.FromDateTime(DateTime.Today),
                SubCount = 0,
                Country = request.Country,
                UserId = user.Id // Явно устанавливаем связь
            };

            await _context.Channels.AddAsync(channel, cancellationToken);
            
            // Создаем плейлисты
            await _playlistService.CreateDefaultPlaylists(channel, cancellationToken);

            // Сохраняем все изменения
            await _context.SaveChangesAsync(cancellationToken);

            // Генерируем код подтверждения
            var code = _emailService.GenerateRandomCode();
            await _userManager.AddClaimAsync(user, new Claim(EmailSuccessMessage.EmailConfirmCodeString, code));

            // Отправляем email
            await _emailService.SendEmailAsync(user.Email, EmailSuccessMessage.EmailConfirmCodeMessage, code);

            // Входим в систему
            await _signInManager.SignInAsync(user, false);

            // Генерируем токен
            var jwtToken = await _jwtService.GenerateAccessToken(user);
            var refreshToken = await _jwtService.GenerateRefreshToken(user, cancellationToken);

            // Фиксируем транзакцию
            await transaction.CommitAsync(cancellationToken);

            return new AuthResponse
            {
                IsSuccessfully = true,
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                UserId = user.Id
            };
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}