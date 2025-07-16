using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto> GetByIdAsync(int id);
        Task<IEnumerable<MessageDto>> GetAllAsync();
        Task AddAsync(MessageDto messageDto);
        Task UpdateAsync(MessageDto messageDto);
        Task DeleteAsync(int id);
    }
} 