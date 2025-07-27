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
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<EnrollmentDto?> GetByIdAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdWithIncludeAsync(id, e => e.Student, e => e.Class);
            return enrollment != null ? _mapper.Map<EnrollmentDto>(enrollment) : null;
        }
        
        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllWithIncludeAsync(e => e.Student, e => e.Class);
            return enrollments != null ? _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments) : new List<EnrollmentDto>();
        }
        
        public async Task AddAsync(EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto == null)
                throw new ArgumentNullException(nameof(enrollmentDto));
                
            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            await _enrollmentRepository.AddAsync(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(EnrollmentDto enrollmentDto)
        {
            if (enrollmentDto == null)
                throw new ArgumentNullException(nameof(enrollmentDto));
                
            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            _enrollmentRepository.Update(enrollment);
            await _enrollmentRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment != null)
            {
                _enrollmentRepository.Remove(enrollment);
                await _enrollmentRepository.SaveChangesAsync();
            }
        }
    }
} 