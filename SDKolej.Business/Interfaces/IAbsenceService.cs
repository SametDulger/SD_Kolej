using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IAbsenceService
    {
        Task<AbsenceDto> GetByIdAsync(int id);
        Task<IEnumerable<AbsenceDto>> GetAllAsync();
        Task AddAsync(AbsenceDto absenceDto);
        Task UpdateAsync(AbsenceDto absenceDto);
        Task DeleteAsync(int id);
    }
} 