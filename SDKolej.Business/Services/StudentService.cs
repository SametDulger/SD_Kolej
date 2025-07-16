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
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }
        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
        public async Task AddAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _studentRepository.Update(student);
            await _studentRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            _studentRepository.Remove(student);
            await _studentRepository.SaveChangesAsync();
        }
    }
} 