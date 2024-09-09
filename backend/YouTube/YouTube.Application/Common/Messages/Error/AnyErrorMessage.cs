namespace YouTube.Application.Common.Messages.Error;

public struct AnyErrorMessage
{
    public const string ErrorMessage = "Что то пошло не так!";
    public const string RequestIsEmpty = "Запрос пустой";
    public const string ClaimsIsEmpty = "Клеймы пусты";
    public const string InvalidConfirmationCode = "Неверный код подтверждения";
}