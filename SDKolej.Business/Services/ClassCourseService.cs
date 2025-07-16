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
    public class ClassCourseService : IClassCourseService
    {
        private readonly IRepository<ClassCourse> _classCourseRepository;
        private readonly IMapper _mapper;

        public ClassCourseService(IRepository<ClassCourse> classCourseRepository, IMapper mapper)
        {
            _classCourseRepository = classCourseRepository;
            _mapper = mapper;
        }

        public async Task<ClassCourseDto> GetByIdAsync(int classId, int courseId)
        {
            var classCourse = (await _classCourseRepository.FindAsync(cc => cc.ClassId == classId && cc.CourseId == courseId)).FirstOrDefault();
            return _mapper.Map<ClassCourseDto>(classCourse);
        }

        public async Task<IEnumerable<ClassCourseDto>> GetAllAsync()
        {
            var classCourses = await _classCourseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassCourseDto>>(classCourses);
        }

        public async Task AddAsync(ClassCourseDto classCourseDto)
        {
            var classCourse = _mapper.Map<ClassCourse>(classCourseDto);
            await _classCourseRepository.AddAsync(classCourse);
            await _classCourseRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClassCourseDto classCourseDto)
        {
            var classCourse = _mapper.Map<ClassCourse>(classCourseDto);
            _classCourseRepository.Update(classCourse);
            await _classCourseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int classId, int courseId)
        {
            var classCourse = (await _classCourseRepository.FindAsync(cc => cc.ClassId == classId && cc.CourseId == courseId)).FirstOrDefault();
            if (classCourse != null)
            {
                _classCourseRepository.Remove(classCourse);
                await _classCourseRepository.SaveChangesAsync();
            }
        }
    }
} 