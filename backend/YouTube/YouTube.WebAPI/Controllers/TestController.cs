using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using YouTube.Application.Interfaces;
namespace YouTube.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IS3Service _service;
    private readonly IEmailService _emailService;

    public TestController(IS3Service service, IEmailService emailService)
    {
        _service = service;
        _emailService = emailService;
    }

    [HttpGet("GetLink")]
    public async Task<IActionResult> GetLink(string bucketId, string objectName, CancellationToken cancellationToken)
    {
        var link = await _service.GetLinkAsync(bucketId, objectName, cancellationToken);

        if (link != String.Empty)
            return Ok(link);

        return BadRequest("Pizda");
    }
    

    [HttpPost("GetFileForUser")]
    public async Task<IActionResult> GetFileForUser()
    {
        IMinioClient client = new MinioClient()
            .WithEndpoint("play.min.io")
            .WithCredentials("ns7G0GX9I5IFWDptZk6b", "OZ4pPiUM5Er7kMzmeDamCeNz2r7UARgqmvSRj1YC")
            .WithSSL(false)
            .Build();

       var link = await client.PresignedGetObjectAsync(
               new PresignedGetObjectArgs()
                   .WithBucket("fortest")
                   .WithObject("IMG_4520.MP4")   
                   .WithExpiry(300))
           .ConfigureAwait(false) ?? String.Empty;

       return Ok(link);

    }
    
    [HttpPost("UploadFileForUser")]
    public async Task<IActionResult> UploadFileForUser(IFormFile file, CancellationToken cancellationToken)
    {
        IMinioClient client = new MinioClient()
            .WithEndpoint("play.min.io:9001")
            .WithCredentials("qdJ9ilmIEJXcDrYCyj9h", "vx2VUCmkxRmEdC11N3sI7veDDptg3dIn3yDbEuy6")
            .WithSSL()
            .Build();
        
        var found = await client.BucketExistsAsync(new BucketExistsArgs().WithBucket("fortest"), cancellationToken);
        if (!found)
        {
            await client.MakeBucketAsync(new MakeBucketArgs().WithBucket("fortest"), cancellationToken);
        }
        
        await using (var stream = file.OpenReadStream())
        {
            var uploadFile = new PutObjectArgs()
                .WithBucket("fortest")
                .WithObject(file.FileName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);
            
            await client.PutObjectAsync(uploadFile, cancellationToken);
        }

        return Ok(file.FileName);

    }
    
    [HttpGet("GetVideoLink")]
    public async Task<IActionResult> GetVideoLink(CancellationToken cancellationToken)
    {
        var kink = await _service.GetObjectAsync("avatar", "IMG_4520.MP4", cancellationToken);
        return Ok(kink);
    }
    
    [HttpGet("EmailTest")]
    public async Task<IActionResult> EmailTest()
    {
        await _emailService.SendEmailAsync("bulatfri18@gmail.com", "Хуй", "ХУЙ");

        return Ok();
    }
}