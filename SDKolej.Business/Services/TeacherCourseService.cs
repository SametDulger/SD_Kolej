using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;
using System.Linq;

namespace SDKolej.Business.Services
{
    public class TeacherCourseService : ITeacherCourseService
    {
        private readonly IRepository<TeacherCourse> _teacherCourseRepository;
        private readonly IMapper _mapper;

        public TeacherCourseService(IRepository<TeacherCourse> teacherCourseRepository, IMapper mapper)
        {
            _teacherCourseRepository = teacherCourseRepository;
            _mapper = mapper;
        }

        public async Task<TeacherCourseDto> GetByIdAsync(int teacherId, int courseId)
        {
            var teacherCourse = (await _teacherCourseRepository.FindAsync(tc => tc.TeacherId == teacherId && tc.CourseId == courseId)).FirstOrDefault();
            return _mapper.Map<TeacherCourseDto>(teacherCourse);
        }

        public async Task<IEnumerable<TeacherCourseDto>> GetAllAsync()
        {
            var teacherCourses = await _teacherCourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherCourseDto>>(teacherCourses);
        }

        public async Task AddAsync(TeacherCourseDto teacherCourseDto)
        {
            var teacherCourse = _mapper.Map<TeacherCourse>(teacherCourseDto);
            await _teacherCourseRepository.AddAsync(teacherCourse);
            await _teacherCourseRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(TeacherCourseDto teacherCourseDto)
        {
            var teacherCourse = _mapper.Map<TeacherCourse>(teacherCourseDto);
            _teacherCourseRepository.Update(teacherCourse);
            await _teacherCourseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int teacherId, int courseId)
        {
            var teacherCourse = (await _teacherCourseRepository.FindAsync(tc => tc.TeacherId == teacherId && tc.CourseId == courseId)).FirstOrDefault();
            if (teacherCourse != null)
            {
                _teacherCourseRepository.Remove(teacherCourse);
                await _teacherCourseRepository.SaveChangesAsync();
            }
        }
    }
} 