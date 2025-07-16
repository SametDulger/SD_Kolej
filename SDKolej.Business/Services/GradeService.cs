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
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }
        public async Task<GradeDto> GetByIdAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            return _mapper.Map<GradeDto>(grade);
        }
        public async Task<IEnumerable<GradeDto>> GetAllAsync()
        {
            var grades = await _gradeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GradeDto>>(grades);
        }
        public async Task AddAsync(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            await _gradeRepository.AddAsync(grade);
            await _gradeRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(GradeDto gradeDto)
        {
            var grade = _mapper.Map<Grade>(gradeDto);
            _gradeRepository.Update(grade);
            await _gradeRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var grade = await _gradeRepository.GetByIdAsync(id);
            _gradeRepository.Remove(grade);
            await _gradeRepository.SaveChangesAsync();
        }
    }
} 