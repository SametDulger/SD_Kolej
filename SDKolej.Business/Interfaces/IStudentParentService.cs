using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IStudentParentService
    {
        Task<StudentParentDto> GetByIdAsync(int studentId, int parentId);
        Task<IEnumerable<StudentParentDto>> GetAllAsync();
        Task AddAsync(StudentParentDto studentParentDto);
        Task UpdateAsync(StudentParentDto studentParentDto);
        Task DeleteAsync(int studentId, int parentId);
    }
} 