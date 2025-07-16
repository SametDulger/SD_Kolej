using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;
        private readonly IMapper _mapper;
        public DocumentService(IRepository<Document> documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }
        public async Task<DocumentDto> GetByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            return _mapper.Map<DocumentDto>(document);
        }
        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _documentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DocumentDto>>(documents);
        }
        public async Task AddAsync(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(DocumentDto documentDto)
        {
            var document = _mapper.Map<Document>(documentDto);
            _documentRepository.Update(document);
            await _documentRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            _documentRepository.Remove(document);
            await _documentRepository.SaveChangesAsync();
        }
    }
} 