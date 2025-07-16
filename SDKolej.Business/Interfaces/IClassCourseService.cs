using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IClassCourseService
    {
        Task<ClassCourseDto> GetByIdAsync(int classId, int courseId);
        Task<IEnumerable<ClassCourseDto>> GetAllAsync();
        Task AddAsync(ClassCourseDto classCourseDto);
        Task UpdateAsync(ClassCourseDto classCourseDto);
        Task DeleteAsync(int classId, int courseId);
    }
} 