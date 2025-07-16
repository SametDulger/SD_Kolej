using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IRepository<Absence> _absenceRepository;
        private readonly IMapper _mapper;
        public AbsenceService(IRepository<Absence> absenceRepository, IMapper mapper)
        {
            _absenceRepository = absenceRepository;
            _mapper = mapper;
        }
        public async Task<AbsenceDto> GetByIdAsync(int id)
        {
            var absence = await _absenceRepository.GetByIdAsync(id);
            return _mapper.Map<AbsenceDto>(absence);
        }
        public async Task<IEnumerable<AbsenceDto>> GetAllAsync()
        {
            var absences = await _absenceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AbsenceDto>>(absences);
        }
        public async Task AddAsync(AbsenceDto absenceDto)
        {
            var absence = _mapper.Map<Absence>(absenceDto);
            await _absenceRepository.AddAsync(absence);
            await _absenceRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(AbsenceDto absenceDto)
        {
            var absence = _mapper.Map<Absence>(absenceDto);
            _absenceRepository.Update(absence);
            await _absenceRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var absence = await _absenceRepository.GetByIdAsync(id);
            _absenceRepository.Remove(absence);
            await _absenceRepository.SaveChangesAsync();
        }
    }
} 