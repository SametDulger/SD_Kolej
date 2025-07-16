using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var grades = await _gradeService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<GradeDto>>.SuccessResult(grades, "Notlar başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Notlar getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var grade = await _gradeService.GetByIdAsync(id);
                if (grade == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Not bulunamadı"));
                }
                return Ok(ApiResponse<GradeDto>.SuccessResult(grade, "Not başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Not getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GradeDto gradeDto)
        {
            try
            {
                await _gradeService.AddAsync(gradeDto);
                return CreatedAtAction(nameof(GetById), new { id = gradeDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Not başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Not oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GradeDto gradeDto)
        {
            try
            {
                if (id != gradeDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _gradeService.UpdateAsync(gradeDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Not başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Not güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gradeService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Not başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Not silinirken hata oluştu"));
            }
        }
    }
} 