namespace YouTube.Domain.Entities;

public class StaticFile
{
    public int Id { get; set; }
    
    public string Path { get; set; }
    
    public Channel Channel { get; set; }
    public Video Video { get; set; }
    
    public UserInfo UserInfo { get; set; }
}