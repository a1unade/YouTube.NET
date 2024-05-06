using Microsoft.AspNetCore.Http;

namespace YouTube.Application.Interfaces;

public interface IVideoService
{
    Task<IEnumerable<string>> GetAllVideo();

    Task AddVideo(IFormFile video);
}