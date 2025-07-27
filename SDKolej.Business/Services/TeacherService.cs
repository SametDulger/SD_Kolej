using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _teacherRepository;
        private readonly IMapper _mapper;
        
        public TeacherService(IRepository<Teacher> teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository ?? throw new ArgumentNullException(nameof(teacherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<TeacherDto?> GetByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdWithIncludeAsync(id, t => t.Branch);
            return teacher != null ? _mapper.Map<TeacherDto>(teacher) : null;
        }
        
        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var teachers = await _teacherRepository.GetAllWithIncludeAsync(t => t.Branch);
            return teachers != null ? _mapper.Map<IEnumerable<TeacherDto>>(teachers) : new List<TeacherDto>();
        }
        
        public async Task AddAsync(TeacherDto teacherDto)
        {
            if (teacherDto == null)
                throw new ArgumentNullException(nameof(teacherDto));
                
            var teacher = _mapper.Map<Teacher>(teacherDto);
            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(TeacherDto teacherDto)
        {
            if (teacherDto == null)
                throw new ArgumentNullException(nameof(teacherDto));
                
            var teacher = _mapper.Map<Teacher>(teacherDto);
            _teacherRepository.Update(teacher);
            await _teacherRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher != null)
            {
                _teacherRepository.Remove(teacher);
                await _teacherRepository.SaveChangesAsync();
            }
        }
    }
} 