using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface ITeacherService
    {
        Task<TeacherDto> GetByIdAsync(int id);
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task AddAsync(TeacherDto teacherDto);
        Task UpdateAsync(TeacherDto teacherDto);
        Task DeleteAsync(int id);
    }
} 