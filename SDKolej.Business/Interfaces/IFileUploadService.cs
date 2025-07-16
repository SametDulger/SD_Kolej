using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;

namespace SDKolej.Business.Interfaces
{
    public interface IFileUploadService
    {
        Task<FileUploadDto> GetByIdAsync(int id);
        Task<IEnumerable<FileUploadDto>> GetAllAsync();
        Task AddAsync(FileUploadDto fileUploadDto);
        Task UpdateAsync(FileUploadDto fileUploadDto);
        Task DeleteAsync(int id);
    }
} 