using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class ClassService : IClassService
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IMapper _mapper;
        public ClassService(IRepository<Class> classRepository, IMapper mapper)
        {
            _classRepository = classRepository;
            _mapper = mapper;
        }
        public async Task<ClassDto> GetByIdAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);
            return _mapper.Map<ClassDto>(classEntity);
        }
        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }
        public async Task AddAsync(ClassDto classDto)
        {
            var classEntity = _mapper.Map<Class>(classDto);
            await _classRepository.AddAsync(classEntity);
            await _classRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(ClassDto classDto)
        {
            var classEntity = _mapper.Map<Class>(classDto);
            _classRepository.Update(classEntity);
            await _classRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);
            _classRepository.Remove(classEntity);
            await _classRepository.SaveChangesAsync();
        }
    }
} 