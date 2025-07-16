using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var teachers = await _teacherService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<TeacherDto>>.SuccessResult(teachers, "Öğretmenler başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmenler getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var teacher = await _teacherService.GetByIdAsync(id);
                if (teacher == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Öğretmen bulunamadı"));
                }
                return Ok(ApiResponse<TeacherDto>.SuccessResult(teacher, "Öğretmen başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmen getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherDto teacherDto)
        {
            try
            {
                await _teacherService.AddAsync(teacherDto);
                return CreatedAtAction(nameof(GetById), new { id = teacherDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Öğretmen başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmen oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherDto teacherDto)
        {
            try
            {
                if (id != teacherDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _teacherService.UpdateAsync(teacherDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Öğretmen başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmen güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _teacherService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Öğretmen başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmen silinirken hata oluştu"));
            }
        }
    }
} 