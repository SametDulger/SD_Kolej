using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<EnrollmentDto>>.SuccessResult(enrollments, "Kayıtlar başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kayıtlar getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetByIdAsync(id);
                if (enrollment == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Kayıt bulunamadı"));
                }
                return Ok(ApiResponse<EnrollmentDto>.SuccessResult(enrollment, "Kayıt başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kayıt getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EnrollmentDto enrollmentDto)
        {
            try
            {
                await _enrollmentService.AddAsync(enrollmentDto);
                return CreatedAtAction(nameof(GetById), new { id = enrollmentDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Kayıt başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kayıt oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnrollmentDto enrollmentDto)
        {
            try
            {
                if (id != enrollmentDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _enrollmentService.UpdateAsync(enrollmentDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Kayıt başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kayıt güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _enrollmentService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Kayıt başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kayıt silinirken hata oluştu"));
            }
        }
    }
} 