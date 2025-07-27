using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IStudentCourseService
    {
        Task<StudentCourseDto?> GetByIdAsync(int id);
        Task<IEnumerable<StudentCourseDto>> GetAllAsync();
        Task<IEnumerable<StudentCourseDto>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentCourseDto>> GetByClassIdAsync(int classId);
        Task<IEnumerable<StudentCourseDto>> GetByCourseIdAsync(int courseId);
        Task<IEnumerable<StudentCourseDto>> GetByTeacherIdAsync(int teacherId);
        Task AddAsync(StudentCourseDto studentCourseDto);
        Task UpdateAsync(StudentCourseDto studentCourseDto);
        Task DeleteAsync(int id);
    }
} 