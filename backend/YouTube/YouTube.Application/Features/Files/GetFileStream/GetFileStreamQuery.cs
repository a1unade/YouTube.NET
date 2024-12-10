using MediatR;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Common.Responses.Files;

namespace YouTube.Application.Features.Files.GetFileStream;

public class GetFileStreamQuery : IdRequest, IRequest<FileStreamResponse>
{
    public GetFileStreamQuery(IdRequest request) : base(request)
    {
        
    }
}