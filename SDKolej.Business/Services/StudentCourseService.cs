using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SDKolej.Business.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly IRepository<StudentCourse> _studentCourseRepository;
        private readonly IMapper _mapper;
        
        public StudentCourseService(IRepository<StudentCourse> studentCourseRepository, IMapper mapper)
        {
            _studentCourseRepository = studentCourseRepository ?? throw new ArgumentNullException(nameof(studentCourseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<StudentCourseDto?> GetByIdAsync(int id)
        {
            var studentCourse = await _studentCourseRepository.GetByIdAsync(id);
            return studentCourse != null ? _mapper.Map<StudentCourseDto>(studentCourse) : null;
        }
        
        public async Task<IEnumerable<StudentCourseDto>> GetAllAsync()
        {
            var studentCourses = await _studentCourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }
        
        public async Task<IEnumerable<StudentCourseDto>> GetByStudentIdAsync(int studentId)
        {
            var studentCourses = await _studentCourseRepository.FindAsync(sc => sc.StudentId == studentId);
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }
        
        public async Task<IEnumerable<StudentCourseDto>> GetByClassIdAsync(int classId)
        {
            var studentCourses = await _studentCourseRepository.FindAsync(sc => sc.ClassId == classId);
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }
        
        public async Task<IEnumerable<StudentCourseDto>> GetByCourseIdAsync(int courseId)
        {
            var studentCourses = await _studentCourseRepository.FindAsync(sc => sc.CourseId == courseId);
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }
        
        public async Task<IEnumerable<StudentCourseDto>> GetByTeacherIdAsync(int teacherId)
        {
            var studentCourses = await _studentCourseRepository.FindAsync(sc => sc.TeacherId == teacherId);
            return _mapper.Map<IEnumerable<StudentCourseDto>>(studentCourses);
        }
        
        public async Task AddAsync(StudentCourseDto studentCourseDto)
        {
            if (studentCourseDto == null)
                throw new ArgumentNullException(nameof(studentCourseDto));
                
            var studentCourse = _mapper.Map<StudentCourse>(studentCourseDto);
            await _studentCourseRepository.AddAsync(studentCourse);
            await _studentCourseRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(StudentCourseDto studentCourseDto)
        {
            if (studentCourseDto == null)
                throw new ArgumentNullException(nameof(studentCourseDto));
                
            var studentCourse = _mapper.Map<StudentCourse>(studentCourseDto);
            _studentCourseRepository.Update(studentCourse);
            await _studentCourseRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var studentCourse = await _studentCourseRepository.GetByIdAsync(id);
            if (studentCourse != null)
            {
                _studentCourseRepository.Remove(studentCourse);
                await _studentCourseRepository.SaveChangesAsync();
            }
        }
    }
} 