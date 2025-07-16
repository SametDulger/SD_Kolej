using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface ITeacherCourseService
    {
        Task<TeacherCourseDto> GetByIdAsync(int teacherId, int courseId);
        Task<IEnumerable<TeacherCourseDto>> GetAllAsync();
        Task AddAsync(TeacherCourseDto teacherCourseDto);
        Task UpdateAsync(TeacherCourseDto teacherCourseDto);
        Task DeleteAsync(int teacherId, int courseId);
    }
} 