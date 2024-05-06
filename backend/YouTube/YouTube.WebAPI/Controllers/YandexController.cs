using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using YouTube.Application.DTOs.DiskResponse;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class YandexController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public YandexController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetDiskInfo()
    {
        string accessToken = _configuration["Yandex:Token"]!;

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken); // Всегда надо добавлять

            string url = "https://cloud-api.yandex.net/v1/disk/";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                    return Ok(responseBody);
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {e.Message}");
                return StatusCode(500); 
            }
        }
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllFilesOnDisk()
    {
        try
        {
            string accessToken = _configuration["Yandex:Token"]!;

            // Создание HTTP клиента
            using (HttpClient client = new HttpClient())
            {
                // Установка заголовка Authorization
                client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken);

                // URL для запроса списка всех файлов
                string url = "https://cloud-api.yandex.net/v1/disk/resources/files";

                // Отправка GET запроса
                HttpResponseMessage response = await client.GetAsync(url);

                // Проверка успешности запроса
                if (response.IsSuccessStatusCode)
                {
                    // Чтение содержимого ответа
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);

                    // Возвращаем ответ в виде строки
                    return Ok(responseBody);
                }
                else
                {
                    // Обработка ошибки
                    Console.WriteLine($"Ошибка: {response.StatusCode}");
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
        catch (Exception ex)
        {
            // Обработка исключения
            Console.WriteLine($"Ошибка: {ex.Message}");
            return StatusCode(500); // Internal Server Error
        }
    }
    
    [HttpPost("[action]")]
        public async Task<IActionResult> UploadFileToDisk()
        {
            try
            {
                string accessToken = _configuration["Yandex:Token"]!;

                // Путь, по которому следует загрузить файл
                string path = "/Gin.png";
                Console.WriteLine(Uri.EscapeDataString(path));
                // URL для запроса на получение ссылки для загрузки файла
                string uploadUrl = $"https://cloud-api.yandex.net/v1/disk/resources/upload?path={Uri.EscapeDataString(path)}&overwrite=true";

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "OAuth " + accessToken);

                    // Отправка GET запроса для получения ссылки для загрузки файла
                    Console.WriteLine("1");
                    HttpResponseMessage response = await client.GetAsync(uploadUrl);
                    Console.WriteLine("2");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);

                        // Чтение содержимого ответа
                        Console.WriteLine("3");

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("4");
                        Console.WriteLine(responseBody);

                        var uploadLink = JsonSerializer.Deserialize<UploadLinkResponse>(responseBody);
                        Console.WriteLine("5");

                        byte[] fileBytes = System.IO.File.ReadAllBytes("C:\\Users\\Булат\\Desktop\\Gin.jpg");
                        Console.WriteLine("6");

                        using (HttpClient uploadClient = new HttpClient())
                        {
                            // Отправка файла на полученный URL для загрузки
                            Console.WriteLine("7");
                            Console.WriteLine(uploadLink.Href  +" 1");
                            Console.WriteLine(uploadLink.Method + " 2");
                            Console.WriteLine(uploadLink.Templated + " 3");
                            
                            
                            HttpResponseMessage uploadResponse = await uploadClient.PutAsync(uploadLink.Href, new ByteArrayContent(fileBytes));
                            Console.WriteLine("8");

                            if (uploadResponse.IsSuccessStatusCode)
                            {
                                return Ok("File uploaded successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Ошибка при загрузке файла: {uploadResponse.StatusCode}");
                                return StatusCode((int)uploadResponse.StatusCode);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: {response.StatusCode}");
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Ошибка: {ex.Message}");
                return StatusCode(500); 
            }
        }
}