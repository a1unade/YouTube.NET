namespace YouTube.Application.Common.Responses;
/// <summary>
/// Базовый ответ
/// </summary>
public class BaseResponse
{
    public BaseResponse(bool isSuccessfully, string message)
    {
        IsSuccessfully = isSuccessfully;
        Message = message;
    }

    public BaseResponse()
    {
        
    }
    /// <summary>
    /// Всё ли успешно выполнено
    /// </summary>
    public bool IsSuccessfully { get; set; } 
    
    /// <summary>
    /// Сообщение
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid? EntityId { get; set; }
}