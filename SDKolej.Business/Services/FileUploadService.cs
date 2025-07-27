using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IRepository<FileUpload> _fileUploadRepository;
        private readonly IMapper _mapper;
        
        public FileUploadService(IRepository<FileUpload> fileUploadRepository, IMapper mapper)
        {
            _fileUploadRepository = fileUploadRepository ?? throw new ArgumentNullException(nameof(fileUploadRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<FileUploadDto?> GetByIdAsync(int id)
        {
            var fileUpload = await _fileUploadRepository.GetByIdAsync(id);
            return fileUpload != null ? _mapper.Map<FileUploadDto>(fileUpload) : null;
        }
        
        public async Task<IEnumerable<FileUploadDto>> GetAllAsync()
        {
            var fileUploads = await _fileUploadRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FileUploadDto>>(fileUploads);
        }
        
        public async Task AddAsync(FileUploadDto fileUploadDto)
        {
            if (fileUploadDto == null)
                throw new ArgumentNullException(nameof(fileUploadDto));
                
            var fileUpload = _mapper.Map<FileUpload>(fileUploadDto);
            await _fileUploadRepository.AddAsync(fileUpload);
            await _fileUploadRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(FileUploadDto fileUploadDto)
        {
            if (fileUploadDto == null)
                throw new ArgumentNullException(nameof(fileUploadDto));
                
            var fileUpload = _mapper.Map<FileUpload>(fileUploadDto);
            _fileUploadRepository.Update(fileUpload);
            await _fileUploadRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var fileUpload = await _fileUploadRepository.GetByIdAsync(id);
            if (fileUpload != null)
            {
                _fileUploadRepository.Remove(fileUpload);
                await _fileUploadRepository.SaveChangesAsync();
            }
        }
    }
} 