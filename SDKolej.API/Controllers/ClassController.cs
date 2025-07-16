using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var classes = await _classService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<ClassDto>>.SuccessResult(classes, "Sınıflar başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıflar getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var classEntity = await _classService.GetByIdAsync(id);
                if (classEntity == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Sınıf bulunamadı"));
                }
                return Ok(ApiResponse<ClassDto>.SuccessResult(classEntity, "Sınıf başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıf getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassDto classDto)
        {
            try
            {
                await _classService.AddAsync(classDto);
                return CreatedAtAction(nameof(GetById), new { id = classDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Sınıf başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıf oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassDto classDto)
        {
            try
            {
                if (id != classDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _classService.UpdateAsync(classDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Sınıf başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıf güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _classService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Sınıf başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıf silinirken hata oluştu"));
            }
        }
    }
} 