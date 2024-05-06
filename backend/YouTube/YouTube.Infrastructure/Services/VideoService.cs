using Microsoft.AspNetCore.Http;
using YouTube.Application.Interfaces;
using YouTube.Domain.Entities;

namespace YouTube.Infrastructure.Services;

public class VideoService : IVideoService
{
    private readonly string _uploadsFolder;

    public VideoService()
    {
        _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    }

    public async Task<IEnumerable<string>> GetAllVideo()
    {
        if (!Directory.Exists(_uploadsFolder))
        {
            return Enumerable.Empty<string>();
        }

        var videoFiles = Directory.GetFiles(_uploadsFolder)
            .Select(filePath => Path.GetFileName(filePath));

        return await Task.FromResult(videoFiles);
    }

    public async Task AddVideo(IFormFile video)
    {
        User user = new User()
        {
            Id = Guid.NewGuid()
        };
        if (!Directory.Exists(_uploadsFolder))
        {
            Directory.CreateDirectory(_uploadsFolder);
        } 
        //var uniqueFileName =  user.Id + Path.GetExtension(video.FileName);


        var filePath = Path.Combine(_uploadsFolder, /*uniqueFileName*/ video.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await video.CopyToAsync(stream);
        }
    }
}