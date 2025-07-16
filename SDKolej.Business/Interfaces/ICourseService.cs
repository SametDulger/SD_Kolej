using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> GetByIdAsync(int id);
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task AddAsync(CourseDto courseDto);
        Task UpdateAsync(CourseDto courseDto);
        Task DeleteAsync(int id);
    }
} 