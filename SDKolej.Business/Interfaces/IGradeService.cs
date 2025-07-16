using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IGradeService
    {
        Task<GradeDto> GetByIdAsync(int id);
        Task<IEnumerable<GradeDto>> GetAllAsync();
        Task AddAsync(GradeDto gradeDto);
        Task UpdateAsync(GradeDto gradeDto);
        Task DeleteAsync(int id);
    }
} 