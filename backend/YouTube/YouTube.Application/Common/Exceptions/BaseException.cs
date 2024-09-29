using System.Net;

namespace YouTube.Application.Common.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    
    public BaseException(string message)
    {
        
    }

    public BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}