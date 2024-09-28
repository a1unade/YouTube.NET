using Microsoft.AspNetCore.Http;

namespace YouTube.Application.DTOs.Video;

public class AddVideoDto
{
    public IFormFile? Video { get; set; }
    
    public string? ImgUrl { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? UserId { get; set; }
}