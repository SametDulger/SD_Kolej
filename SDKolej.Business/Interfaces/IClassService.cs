using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IClassService
    {
        Task<ClassDto> GetByIdAsync(int id);
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task AddAsync(ClassDto classDto);
        Task UpdateAsync(ClassDto classDto);
        Task DeleteAsync(int id);
    }
} 