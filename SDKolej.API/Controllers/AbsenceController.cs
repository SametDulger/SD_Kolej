using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.Interfaces;
using SDKolej.Business.DTOs;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;
        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var absences = await _absenceService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<AbsenceDto>>.SuccessResult(absences, "Devamsızlık kayıtları başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Devamsızlık kayıtları getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var absence = await _absenceService.GetByIdAsync(id);
                if (absence == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Devamsızlık kaydı bulunamadı"));
                }
                return Ok(ApiResponse<AbsenceDto>.SuccessResult(absence, "Devamsızlık kaydı başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Devamsızlık kaydı getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AbsenceDto dto)
        {
            try
            {
                await _absenceService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Devamsızlık kaydı başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Devamsızlık kaydı oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AbsenceDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _absenceService.UpdateAsync(dto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Devamsızlık kaydı başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Devamsızlık kaydı güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _absenceService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Devamsızlık kaydı başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Devamsızlık kaydı silinirken hata oluştu"));
            }
        }
    }
} 