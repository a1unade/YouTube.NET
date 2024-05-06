using System.Text.Json.Serialization;

namespace YouTube.Application.DTOs.DiskResponse;

public class UploadLinkResponse
{
    [JsonPropertyName("href")]
    public string Href { get; set; }
    [JsonPropertyName("method")]
    public string Method { get; set; }
    [JsonPropertyName("templated")]
    public bool Templated { get; set; }
    
}