using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.Interfaces;
using SDKolej.Business.DTOs;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        public FileUploadController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var files = await _fileUploadService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<FileUploadDto>>.SuccessResult(files, "Dosyalar başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Dosyalar getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var file = await _fileUploadService.GetByIdAsync(id);
                if (file == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Dosya bulunamadı"));
                }
                return Ok(ApiResponse<FileUploadDto>.SuccessResult(file, "Dosya başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Dosya getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FileUploadDto dto)
        {
            try
            {
                await _fileUploadService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Dosya başarıyla yüklendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Dosya yüklenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fileUploadService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Dosya başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Dosya silinirken hata oluştu"));
            }
        }
    }
} 