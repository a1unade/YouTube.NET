namespace YouTube.Application.Common.Messages.Success;

public struct AuthSuccessMessages
{
    public const string RegisterSuccess = "Пользователь зарегистрирован. Проверьте вашу электронную почту для подтверждения.";
    public const string EmailConfirmed = "Почта успешно подтверждена.";
    public const string PasswordChanged = "Пароль успешно изменен.";
    public const string EmailConfirmMessageSend = "Повторное письмо с подтверждением отправлено на почту";
}