using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.Interfaces;
using SDKolej.Business.DTOs;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var messages = await _messageService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<MessageDto>>.SuccessResult(messages, "Mesajlar başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Mesajlar getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var message = await _messageService.GetByIdAsync(id);
                if (message == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Mesaj bulunamadı"));
                }
                return Ok(ApiResponse<MessageDto>.SuccessResult(message, "Mesaj başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Mesaj getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageDto dto)
        {
            try
            {
                await _messageService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Mesaj başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Mesaj oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MessageDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _messageService.UpdateAsync(dto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Mesaj başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Mesaj güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _messageService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Mesaj başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Mesaj silinirken hata oluştu"));
            }
        }
    }
} 