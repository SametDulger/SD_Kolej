using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class GradeService : IGradeService
    {
        private readonly IRepository<Grade> _gradeRepository;
        private readonly IMapper _mapper;
        
        public GradeService(IRepository<Grade> gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository ?? throw new ArgumentNullException(nameof(gradeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<GradeDto?> GetByIdAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdWithIncludeAsync(id, g => g.Student, g => g.Course);
            return grade != null ? _mapper.Map<GradeDto>(grade) : null;
        }
        
        public async Task<IEnumerable<GradeDto>> GetAllAsync()
        {
            var grades = await _gradeRepository.GetAllWithIncludeAsync(g => g.Student, g => g.Course);
            return grades != null ? _mapper.Map<IEnumerable<GradeDto>>(grades) : new List<GradeDto>();
        }
        
        public async Task AddAsync(GradeDto gradeDto)
        {
            if (gradeDto == null)
                throw new ArgumentNullException(nameof(gradeDto));
                
            var grade = _mapper.Map<Grade>(gradeDto);
            await _gradeRepository.AddAsync(grade);
            await _gradeRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(GradeDto gradeDto)
        {
            if (gradeDto == null)
                throw new ArgumentNullException(nameof(gradeDto));
                
            var grade = _mapper.Map<Grade>(gradeDto);
            _gradeRepository.Update(grade);
            await _gradeRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            if (grade != null)
            {
                _gradeRepository.Remove(grade);
                await _gradeRepository.SaveChangesAsync();
            }
        }
    }
} 