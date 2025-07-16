using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.API.Models;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var branches = await _branchService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<BranchDto>>.SuccessResult(branches, "Şubeler başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Şubeler getirilirken hata oluştu"));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var branch = await _branchService.GetByIdAsync(id);
                if (branch == null)
                {
                    return NotFound(ApiResponse<object>.ErrorResult("Şube bulunamadı"));
                }
                return Ok(ApiResponse<BranchDto>.SuccessResult(branch, "Şube başarıyla getirildi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Şube getirilirken hata oluştu"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchDto branchDto)
        {
            try
            {
                await _branchService.AddAsync(branchDto);
                return CreatedAtAction(nameof(GetById), new { id = branchDto.Id }, 
                    ApiResponse<object>.SuccessResult(null, "Şube başarıyla oluşturuldu"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Şube oluşturulurken hata oluştu"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BranchDto branchDto)
        {
            try
            {
                if (id != branchDto.Id)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("ID uyumsuzluğu"));
                }

                await _branchService.UpdateAsync(branchDto);
                return Ok(ApiResponse<object>.SuccessResult(null, "Şube başarıyla güncellendi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Şube güncellenirken hata oluştu"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _branchService.DeleteAsync(id);
                return Ok(ApiResponse<object>.SuccessResult(null, "Şube başarıyla silindi"));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResult("Şube silinirken hata oluştu"));
            }
        }
    }
} 