using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using YouTube.Application.Common.Responses;
using YouTube.Application.Common.Responses.DiskResponse;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using YouTube.Domain.Entities;
using YouTube.Persistence.Contexts;

namespace YouTube.Infrastructure.Services;

public class YandexService : IYandexService
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IVideoRepository _videoRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly ApplicationDbContext _context;

    public YandexService(IConfiguration configuration,
        IWebHostEnvironment hostEnvironment,
        IVideoRepository videoRepository,
        IChannelRepository channelRepository,
        ApplicationDbContext context)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
        _videoRepository = videoRepository;
        _channelRepository = channelRepository;
        _context = context;
    }

    public async Task<string> GetAllFilesOnDisk(CancellationToken cancellationToken)
    {
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда

                // URL для запроса списка всех файлов
                string url = "https://cloud-api.yandex.net/v1/disk/resources/files";

                // Отправка GET запроса
                HttpResponseMessage response = await client.GetAsync(url, cancellationToken: cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                    Console.WriteLine(responseBody);

                    return responseBody;
                }

                return "Не получилось";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return "Sad";
        }
    }

   public async Task<UploadFileResponse> UploadFileToDisk(
        IFormFile file,
        string img,
        string name,
        string description,
        string userId,
        CancellationToken cancellationToken)
    {
        var t = await _channelRepository.GetByUser(Guid.Parse(userId), cancellationToken);

        var preview = new StaticFile()
        {
            Path = img
        };

        await _context.StaticFiles.AddAsync(preview, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var video = new Video()
        {
            ChannelId = t.Id,
            Name = name,
            Description = description,
            ReleaseDate = DateTime.Today,
            Channel = t,
            PathInDisk = "/" + userId + t.Id + name + ".mp4",
            PreviewImgId = preview.Id
        };

        await _context.Videos.AddAsync(video, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        string accessToken = _configuration["Yandex:Token"]!;

        // URL для запроса на получение ссылки для загрузки файла
        string uploadUrl =
            $"https://cloud-api.yandex.net/v1/disk/resources/upload?path={Uri.EscapeDataString(video.PathInDisk)}&overwrite=true";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken);

            // Отправка GET запроса для получения ссылки для загрузки файла
            HttpResponseMessage response = await client.GetAsync(uploadUrl, cancellationToken: cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
                var uploadLink = JsonSerializer.Deserialize<LinkResponse>(responseBody);

                // Чтение содержимого файла в массив байтов
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream, cancellationToken);
                    byte[] fileBytes = memoryStream.ToArray();

                    using (HttpClient uploadClient = new HttpClient())
                    {
                        // Отправка файла на полученный URL для загрузки
                        HttpResponseMessage uploadResponse =
                            await uploadClient.PutAsync(uploadLink?.Href, new ByteArrayContent(fileBytes), cancellationToken: cancellationToken);

                        if (uploadResponse.IsSuccessStatusCode)
                        {
                            return new UploadFileResponse
                            {
                                IsSuccessfully = true,
                                Message = "File uploaded successfully."
                            };
                        }

                        return new UploadFileResponse
                        {
                            IsSuccessfully = false,
                            Error = new List<string> { $"Ошибка при загрузке файла: {uploadResponse.StatusCode}" }
                        };
                    }
                }
            }

            return new UploadFileResponse
            {
                IsSuccessfully = false,
                Error = new List<string> { $"Ошибка: {response.StatusCode}" }
            };
        }
    }
   
       public async Task<GetFileFromDiskResponse> GetFile(string path, CancellationToken cancellationToken)
    {
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда

                string url = $"https://cloud-api.yandex.net/v1/disk/resources/download?path={Uri.EscapeDataString(path)}";

                HttpResponseMessage response = await client.GetAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    
                    string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    var json = JsonSerializer.Deserialize<LinkResponse>(responseBody);
                    Console.WriteLine(responseBody);
                    Console.WriteLine(json);
                    var res = await DownloadFile(json.Href, Path.Combine(_hostEnvironment.WebRootPath, "videos"), path, cancellationToken);
                    if (res)
                    {
                        return new GetFileFromDiskResponse
                        {
                            IsSuccessfully = true,
                            Message = $"Получил файл по пути {path}",
                            File = json.Href,
                            PathInDisk = path
                        };
                    }

                    return new GetFileFromDiskResponse()
                    {
                        IsSuccessfully = true,
                        Message =
                            $"Не получилось загрузить файл в {Path.Combine(_hostEnvironment.WebRootPath, "videos")}"
                    };
                }
                    

                return new GetFileFromDiskResponse
                {
                    IsSuccessfully = false,
                    Error = new List<string> { $"не смог получить файл по пути {path}" }
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new GetFileFromDiskResponse
            {
                IsSuccessfully = false,
                Error = new List<string> { $"Ошибка: {ex.Message}" }
            };
        }
    }



    
    public async Task<bool> DownloadFile(string fileUrl, string destinationPath, string path, CancellationToken cancellationToken)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(fileUrl, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken))
                    {
                        string fileName = Path.GetFileName(path); // Получаем имя файла из URL
                        string filePath = Path.Combine(destinationPath, fileName); // Формируем путь к месту назначения

                        using (FileStream fileStream = File.Create(filePath))
                            await stream.CopyToAsync(fileStream, cancellationToken);
                    }
                    return true;
                }
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return false;
        }
    }

    
    
    public async Task<GetFileFromDiskResponse> PublishFile(string path, CancellationToken cancellationToken)
    { 
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда

                string url = $"https://cloud-api.yandex.net/v1/disk/resources/publish?path={Uri.EscapeDataString(path)}";

                HttpResponseMessage response = await client.PutAsync(url, null!, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    var json = JsonSerializer.Deserialize<LinkResponse>(responseBody);
                    Console.WriteLine(json);

                    return new GetFileFromDiskResponse
                    {
                        IsSuccessfully = true,
                        Message = $"Опубликовал файл {path}",
                        LinkResponse = json
                    };
                }

                return new GetFileFromDiskResponse
                {
                    IsSuccessfully = false,
                    Error = new List<string> { $"не смог опубликовать по пути {path}" }
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new GetFileFromDiskResponse
            {
                IsSuccessfully = false,
                Error = new List<string> { $"Ошибка: {ex.Message}" }
            };
        }
    }
    public async Task<CreateFolderResponse> CreateFolder(string path, CancellationToken cancellationToken)
    {
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда

                string url = $"https://cloud-api.yandex.net/v1/disk/resources?path={Uri.EscapeDataString(path)}";

                HttpResponseMessage response = await client.PutAsync(url, null, cancellationToken:cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    Console.WriteLine(responseBody);
                    return new CreateFolderResponse()
                    {
                        IsSuccessfully = true,
                        Message = $"Создал папку по пути {path}"
                    };
                }

                return new CreateFolderResponse()
                {
                    IsSuccessfully = false,
                    Error = new List<string>() { $"не смог создать папку по пути {path}" }
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new CreateFolderResponse()
            {
                IsSuccessfully = false,
                Error = new List<string>() { $"Ошибка: {ex.Message}" }
            };
        }
    }
    public async Task<BaseResponse> DeleteFile(string path, CancellationToken cancellationToken)
    {
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Добавлять всегда

                string url = $"https://cloud-api.yandex.net/v1/disk/resources?path={Uri.EscapeDataString(path)}";

                HttpResponseMessage response = await client.DeleteAsync(url, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    Console.WriteLine(responseBody);
                    return new BaseResponse
                    {
                        IsSuccessfully = true,
                        Message = $"Удалил папку по пути {path}"
                    };
                }

                return new BaseResponse
                {
                    IsSuccessfully = false,
                    Error = new List<string> { $"не смог удалить папку по пути {path}" }
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return new BaseResponse
            {
                IsSuccessfully = false,
                Error = new List<string> { $"Ошибка: {ex.Message}" }
            };
        }
    }
}