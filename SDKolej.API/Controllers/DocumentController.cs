using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.Interfaces;
using SDKolej.Business.DTOs;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var documents = await _documentService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<DocumentDto>>.SuccessResult(documents, "Belgeler başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Belgeler getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var document = await _documentService.GetByIdAsync(id);
                if (document == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Belge bulunamadı"));
                }
                return Ok(ApiResponse<DocumentDto>.SuccessResult(document, "Belge başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Belge getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DocumentDto dto)
        {
            try
            {
                await _documentService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Belge başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Belge oluşturulurken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _documentService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Belge başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Belge silinirken hata oluştu"));
            }
        }
    }
} 