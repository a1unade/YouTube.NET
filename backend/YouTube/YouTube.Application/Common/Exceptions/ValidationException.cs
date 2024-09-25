namespace YouTube.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {
    }
    
    public ValidationException() : base("Произошла ошибока валидации.")
    {
    }

}