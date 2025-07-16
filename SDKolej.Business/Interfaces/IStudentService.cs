using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetByIdAsync(int id);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task AddAsync(StudentDto studentDto);
        Task UpdateAsync(StudentDto studentDto);
        Task DeleteAsync(int id);
    }
} 