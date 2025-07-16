using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IAnnouncementService
    {
        Task<AnnouncementDto> GetByIdAsync(int id);
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();
        Task AddAsync(AnnouncementDto announcementDto);
        Task UpdateAsync(AnnouncementDto announcementDto);
        Task DeleteAsync(int id);
    }
} 