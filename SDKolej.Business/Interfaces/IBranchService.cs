using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDto> GetByIdAsync(int id);
        Task<IEnumerable<BranchDto>> GetAllAsync();
        Task AddAsync(BranchDto branchDto);
        Task UpdateAsync(BranchDto branchDto);
        Task DeleteAsync(int id);
    }
} 