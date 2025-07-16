using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto> GetByIdAsync(int id);
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task AddAsync(EnrollmentDto enrollmentDto);
        Task UpdateAsync(EnrollmentDto enrollmentDto);
        Task DeleteAsync(int id);
    }
} 