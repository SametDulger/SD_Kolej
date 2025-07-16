using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;

        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var parents = await _parentService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<ParentDto>>.SuccessResult(parents, "Veliler başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Veliler getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var parent = await _parentService.GetByIdAsync(id);
                if (parent == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Veli bulunamadı"));
                }
                return Ok(ApiResponse<ParentDto>.SuccessResult(parent, "Veli başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Veli getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParentDto parentDto)
        {
            try
            {
                await _parentService.AddAsync(parentDto);
                return CreatedAtAction(nameof(GetById), new { id = parentDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Veli başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Veli oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ParentDto parentDto)
        {
            try
            {
                if (id != parentDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _parentService.UpdateAsync(parentDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Veli başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Veli güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _parentService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Veli başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Veli silinirken hata oluştu"));
            }
        }
    }
} 