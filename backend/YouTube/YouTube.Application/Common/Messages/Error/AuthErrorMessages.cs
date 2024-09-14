namespace YouTube.Application.Common.Messages.Error;

public struct AuthErrorMessages
{
    public const string RegisterError = "Не получилось зарегистрироваться";
    public const string UserNotFound = "Пользователь не найден.";
    public const string EmailIsBusy = "Пользователь с таким адресом электронной почты уже существует.";
    public const string EmailConfirmationError = "Ошибка при подтверждении почты.";
    public const string LoginWrongPassword = "Неправильный пароль!";
    public const string ErrorUpdatingUser = "Произошла ошибка при обновлении данных";
    public const string EmailNotConfirmed = "Пользователь не подтвердил свою почту.";
    public const string EmailAlreadyConfirmed = "Пользователь уже подтвердил свою почту.";
    public const string ChangePasswordError = "Ошибка при изменении пароля.";
}