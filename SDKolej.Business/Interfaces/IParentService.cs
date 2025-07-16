using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IParentService
    {
        Task<ParentDto> GetByIdAsync(int id);
        Task<IEnumerable<ParentDto>> GetAllAsync();
        Task AddAsync(ParentDto parentDto);
        Task UpdateAsync(ParentDto parentDto);
        Task DeleteAsync(int id);
    }
} 