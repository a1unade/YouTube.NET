using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Interfaces;

namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class YandexController : ControllerBase
{
    private readonly IYandexService _yandexService;
    private readonly IConfiguration _configuration;

    public YandexController(IYandexService yandexService, IConfiguration configuration)
    {
        _yandexService = yandexService;
        _configuration = configuration;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllFilesOnDisk(CancellationToken cancellationToken)
    {
        var yandex = await _yandexService.GetAllFilesOnDisk(cancellationToken);

        return Ok(yandex);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFileToDisk(IFormFile video, CancellationToken cancellationToken)
    {
        var result = await _yandexService.UploadFileToDisk(video, "/FromYt", cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateFolder(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.CreateFolder(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> DeleteFile(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.DeleteFile(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetFileFromPath(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.GetFile(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> PublishFile(string path, CancellationToken cancellationToken)
    {
        var result = await _yandexService.PublishFile(path, cancellationToken);

        if (!result.IsSuccessfully)
            return BadRequest(result);

        return Ok(result);
    }

    // [HttpGet("[action]")]
    // public async Task<IActionResult> GetFile(string url, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         string accessToken = _configuration["Yandex:Token"]!;
    //
    //         using (HttpClient client = new HttpClient())
    //         {
    //             client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда
    //
    //
    //             HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
    //
    //             if (response.IsSuccessStatusCode)
    //             {
    //                 string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
    //                 Console.WriteLine(responseBody);
    //                 return Ok();
    //
    //             }
    //
    //             return BadRequest();
    //
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest();
    //     }
    // }

      [HttpGet("[action]")]
      public async Task<string> GetPublishFile(string publicKey, CancellationToken cancellationToken)
      {
          try
          {
              string accessToken = _configuration["Yandex:Token"]!;

              using (HttpClient client = new HttpClient())
              {
                  client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда


                  HttpResponseMessage response = await client.GetAsync(publicKey,cancellationToken);

                  if (response.IsSuccessStatusCode)
                  {
                      string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                      Console.WriteLine("________________________________________________________________");
                      Console.WriteLine(responseBody);
                      return responseBody;

                  } 
                  return "Хай";

              }
          }
          catch (Exception ex)
          {
              return ex.Message;
          }
      }
}