using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        
        public StudentService(IRepository<Student> studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdWithIncludeAsync(id, s => s.Class);
            return student != null ? _mapper.Map<StudentDto>(student) : null;
        }
        
        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllWithIncludeAsync(s => s.Class);
            return students != null ? _mapper.Map<IEnumerable<StudentDto>>(students) : new List<StudentDto>();
        }
        
        public async Task AddAsync(StudentDto studentDto)
        {
            if (studentDto == null)
                throw new ArgumentNullException(nameof(studentDto));
                
            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(StudentDto studentDto)
        {
            if (studentDto == null)
                throw new ArgumentNullException(nameof(studentDto));
                
            var student = _mapper.Map<Student>(studentDto);
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student != null)
            {
                _studentRepository.Remove(student);
                await _studentRepository.SaveChangesAsync();
            }
        }
    }
} 