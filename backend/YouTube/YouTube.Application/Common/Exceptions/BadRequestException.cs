using System.Net;

namespace YouTube.Application.Common.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base (message, HttpStatusCode.BadRequest)
    {
        
    }
}