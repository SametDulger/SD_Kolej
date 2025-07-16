using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.Interfaces;
using SDKolej.Business.DTOs;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var announcements = await _announcementService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<AnnouncementDto>>.SuccessResult(announcements, "Duyurular başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Duyurular getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var announcement = await _announcementService.GetByIdAsync(id);
                if (announcement == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Duyuru bulunamadı"));
                }
                return Ok(ApiResponse<AnnouncementDto>.SuccessResult(announcement, "Duyuru başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Duyuru getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AnnouncementDto dto)
        {
            try
            {
                await _announcementService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Duyuru başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Duyuru oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnnouncementDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _announcementService.UpdateAsync(dto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Duyuru başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Duyuru güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _announcementService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Duyuru başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Duyuru silinirken hata oluştu"));
            }
        }
    }
} 