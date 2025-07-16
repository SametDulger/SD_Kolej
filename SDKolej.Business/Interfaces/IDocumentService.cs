using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentDto> GetByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        Task AddAsync(DocumentDto documentDto);
        Task UpdateAsync(DocumentDto documentDto);
        Task DeleteAsync(int id);
    }
} 