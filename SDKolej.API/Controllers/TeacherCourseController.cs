using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherCourseController : ControllerBase
    {
        private readonly ITeacherCourseService _teacherCourseService;

        public TeacherCourseController(ITeacherCourseService teacherCourseService)
        {
            _teacherCourseService = teacherCourseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherCourseDto>>> GetAll()
        {
            var teacherCourses = await _teacherCourseService.GetAllAsync();
            return Ok(teacherCourses);
        }

        [HttpGet("{teacherId}/{courseId}")]
        public async Task<ActionResult<TeacherCourseDto>> GetById(int teacherId, int courseId)
        {
            var teacherCourse = await _teacherCourseService.GetByIdAsync(teacherId, courseId);
            if (teacherCourse == null)
                return NotFound();
            return Ok(teacherCourse);
        }

        [HttpPost]
        public async Task<ActionResult> Create(TeacherCourseDto teacherCourseDto)
        {
            await _teacherCourseService.AddAsync(teacherCourseDto);
            return CreatedAtAction(nameof(GetById), new { teacherId = teacherCourseDto.TeacherId, courseId = teacherCourseDto.CourseId }, teacherCourseDto);
        }

        [HttpPut("{teacherId}/{courseId}")]
        public async Task<ActionResult> Update(int teacherId, int courseId, TeacherCourseDto teacherCourseDto)
        {
            if (teacherId != teacherCourseDto.TeacherId || courseId != teacherCourseDto.CourseId)
                return BadRequest();

            await _teacherCourseService.UpdateAsync(teacherCourseDto);
            return NoContent();
        }

        [HttpDelete("{teacherId}/{courseId}")]
        public async Task<ActionResult> Delete(int teacherId, int courseId)
        {
            await _teacherCourseService.DeleteAsync(teacherId, courseId);
            return NoContent();
        }
    }
} 