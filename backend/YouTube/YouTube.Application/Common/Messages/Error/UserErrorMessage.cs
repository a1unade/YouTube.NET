namespace YouTube.Application.Common.Messages.Error;

public struct UserErrorMessage
{
    public const string UserNotFound = "Пользователь не найден!";
    public const string UserEmailsDontMatch = "Почта не совпадает с той, которую вы указывали ранее";
    public const string UserIdIsNotCorrect = "Идентификатор пользователя не правильный";
    public const string EmailNotConfirmed = "Почта не подтверждена";
}