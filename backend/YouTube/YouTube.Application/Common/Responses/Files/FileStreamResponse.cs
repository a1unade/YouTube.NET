namespace YouTube.Application.Common.Responses.Files;

public class FileStreamResponse : BaseResponse
{
    public Stream Stream { get; set; } = default!;

    public string FileName { get; set; } = default!;

    public string ContentType { get; set; } = default!;
}