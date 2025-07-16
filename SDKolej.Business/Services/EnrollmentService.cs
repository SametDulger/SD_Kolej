using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly IMapper _mapper;
        public EnrollmentService(IRepository<Enrollment> enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<EnrollmentDto> GetByIdAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            return _mapper.Map<EnrollmentDto>(enrollment);
        }
        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        }
        public async Task AddAsync(EnrollmentDto enrollmentDto)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(EnrollmentDto enrollmentDto)
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            _enrollmentRepository.Update(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            _enrollmentRepository.Remove(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }
    }
} 