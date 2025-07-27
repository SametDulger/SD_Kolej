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
            _classRepository = classRepository ?? throw new ArgumentNullException(nameof(classRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<ClassDto?> GetByIdAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdWithIncludeAsync(id, c => c.Branch);
            return classEntity != null ? _mapper.Map<ClassDto>(classEntity) : null;
        }
        
        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var classes = await _classRepository.GetAllWithIncludeAsync(c => c.Branch);
            return classes != null ? _mapper.Map<IEnumerable<ClassDto>>(classes) : new List<ClassDto>();
        }
        
        public async Task AddAsync(ClassDto classDto)
        {
            if (classDto == null)
                throw new ArgumentNullException(nameof(classDto));
                
            var classEntity = _mapper.Map<Class>(classDto);
            await _classRepository.AddAsync(classEntity);
            await _classRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(ClassDto classDto)
        {
            if (classDto == null)
                throw new ArgumentNullException(nameof(classDto));
                
            var classEntity = _mapper.Map<Class>(classDto);
            _classRepository.Update(classEntity);
            await _classRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var classEntity = await _classRepository.GetByIdAsync(id);
            if (classEntity != null)
            {
                _classRepository.Remove(classEntity);
                await _classRepository.SaveChangesAsync();
            }
        }
    }
} 