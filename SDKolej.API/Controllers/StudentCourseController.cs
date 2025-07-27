using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;

        public StudentCourseController(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var studentCourses = await _studentCourseService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<StudentCourseDto>>.SuccessResult(studentCourses, "Öğrenci dersleri başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersleri getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var studentCourse = await _studentCourseService.GetByIdAsync(id);
                if (studentCourse == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Öğrenci dersi bulunamadı"));
                }
                return Ok(ApiResponse<StudentCourseDto>.SuccessResult(studentCourse, "Öğrenci dersi başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersi getirilirken hata oluştu"));
            }
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            try
            {
                var studentCourses = await _studentCourseService.GetByStudentIdAsync(studentId);
                return Ok(ApiResponse<IEnumerable<StudentCourseDto>>.SuccessResult(studentCourses, "Öğrenci dersleri başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersleri getirilirken hata oluştu"));
            }
        }

        [HttpGet("class/{classId}")]
        public async Task<IActionResult> GetByClassId(int classId)
        {
            try
            {
                var studentCourses = await _studentCourseService.GetByClassIdAsync(classId);
                return Ok(ApiResponse<IEnumerable<StudentCourseDto>>.SuccessResult(studentCourses, "Sınıf dersleri başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Sınıf dersleri getirilirken hata oluştu"));
            }
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetByCourseId(int courseId)
        {
            try
            {
                var studentCourses = await _studentCourseService.GetByCourseIdAsync(courseId);
                return Ok(ApiResponse<IEnumerable<StudentCourseDto>>.SuccessResult(studentCourses, "Ders öğrencileri başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Ders öğrencileri getirilirken hata oluştu"));
            }
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacherId(int teacherId)
        {
            try
            {
                var studentCourses = await _studentCourseService.GetByTeacherIdAsync(teacherId);
                return Ok(ApiResponse<IEnumerable<StudentCourseDto>>.SuccessResult(studentCourses, "Öğretmen dersleri başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğretmen dersleri getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCourseDto studentCourseDto)
        {
            try
            {
                await _studentCourseService.AddAsync(studentCourseDto);
                return CreatedAtAction(nameof(GetById), new { id = studentCourseDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Öğrenci dersi başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersi oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentCourseDto studentCourseDto)
        {
            try
            {
                if (id != studentCourseDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _studentCourseService.UpdateAsync(studentCourseDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Öğrenci dersi başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersi güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentCourseService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Öğrenci dersi başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Öğrenci dersi silinirken hata oluştu"));
            }
        }
    }
} 