using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassCourseController : ControllerBase
    {
        private readonly IClassCourseService _classCourseService;

        public ClassCourseController(IClassCourseService classCourseService)
        {
            _classCourseService = classCourseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassCourseDto>>> GetAll()
        {
            var classCourses = await _classCourseService.GetAllAsync();
            return Ok(classCourses);
        }

        [HttpGet("{classId}/{courseId}")]
        public async Task<ActionResult<ClassCourseDto>> GetById(int classId, int courseId)
        {
            var classCourse = await _classCourseService.GetByIdAsync(classId, courseId);
            if (classCourse == null)
                return NotFound();
            return Ok(classCourse);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClassCourseDto classCourseDto)
        {
            await _classCourseService.AddAsync(classCourseDto);
            return CreatedAtAction(nameof(GetById), new { classId = classCourseDto.ClassId, courseId = classCourseDto.CourseId }, classCourseDto);
        }

        [HttpPut("{classId}/{courseId}")]
        public async Task<ActionResult> Update(int classId, int courseId, ClassCourseDto classCourseDto)
        {
            if (classId != classCourseDto.ClassId || courseId != classCourseDto.CourseId)
                return BadRequest();

            await _classCourseService.UpdateAsync(classCourseDto);
            return NoContent();
        }

        [HttpDelete("{classId}/{courseId}")]
        public async Task<ActionResult> Delete(int classId, int courseId)
        {
            await _classCourseService.DeleteAsync(classId, courseId);
            return NoContent();
        }
    }
} 