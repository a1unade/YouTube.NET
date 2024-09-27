namespace YouTube.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
    
    public NotFoundException() : base("Сущность не найдена")
    {
    }
}