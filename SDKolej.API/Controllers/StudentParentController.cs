using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentParentController : ControllerBase
    {
        private readonly IStudentParentService _studentParentService;

        public StudentParentController(IStudentParentService studentParentService)
        {
            _studentParentService = studentParentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentParentDto>>> GetAll()
        {
            var studentParents = await _studentParentService.GetAllAsync();
            return Ok(studentParents);
        }

        [HttpGet("{studentId}/{parentId}")]
        public async Task<ActionResult<StudentParentDto>> GetById(int studentId, int parentId)
        {
            var studentParent = await _studentParentService.GetByIdAsync(studentId, parentId);
            if (studentParent == null)
                return NotFound();
            return Ok(studentParent);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentParentDto studentParentDto)
        {
            await _studentParentService.AddAsync(studentParentDto);
            return CreatedAtAction(nameof(GetById), new { studentId = studentParentDto.StudentId, parentId = studentParentDto.ParentId }, studentParentDto);
        }

        [HttpPut("{studentId}/{parentId}")]
        public async Task<ActionResult> Update(int studentId, int parentId, StudentParentDto studentParentDto)
        {
            if (studentId != studentParentDto.StudentId || parentId != studentParentDto.ParentId)
                return BadRequest();

            await _studentParentService.UpdateAsync(studentParentDto);
            return NoContent();
        }

        [HttpDelete("{studentId}/{parentId}")]
        public async Task<ActionResult> Delete(int studentId, int parentId)
        {
            await _studentParentService.DeleteAsync(studentId, parentId);
            return NoContent();
        }
    }
} 