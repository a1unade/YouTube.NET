using Microsoft.AspNetCore.Mvc;
using YouTube.Application.Common.Exceptions;
using YouTube.Application.Common.Requests.Base;
using YouTube.Application.Interfaces;
using YouTube.Application.Interfaces.Repositories;
using File = YouTube.Domain.Entities.File;

namespace YouTube.WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IS3Service _s3Service;
    private readonly IGenericRepository<File> _repository;

    public FileController(IS3Service s3Service, IGenericRepository<File> repository)
    {
        _s3Service = s3Service;
        _repository = repository;
    }
    
    /// <summary>
    /// Получить stream файла
    /// </summary>
    /// <param name="idRequest">Id Файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Stream файла</returns>
    [HttpGet("GetFileStream/{id}")]
    public async Task<IActionResult> GetFileStreamAsync(IdRequest idRequest, CancellationToken cancellationToken)
    {
        var file = await _repository.GetById(idRequest.Id, cancellationToken) ?? throw new NotFoundException();
        await _s3Service.StreamFileAsync(file.BucketName, file.FileName, file.ContentType!, Response, cancellationToken);
        return new EmptyResult();
    }
}