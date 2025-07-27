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
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<DocumentDto?> GetByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdWithIncludeAsync(id, d => d.Student);
            return document != null ? _mapper.Map<DocumentDto>(document) : null;
        }
        
        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _documentRepository.GetAllWithIncludeAsync(d => d.Student);
            return documents != null ? _mapper.Map<IEnumerable<DocumentDto>>(documents) : new List<DocumentDto>();
        }
        
        public async Task AddAsync(DocumentDto documentDto)
        {
            if (documentDto == null)
                throw new ArgumentNullException(nameof(documentDto));
                
            var document = _mapper.Map<Document>(documentDto);
            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(DocumentDto documentDto)
        {
            if (documentDto == null)
                throw new ArgumentNullException(nameof(documentDto));
                
            var document = _mapper.Map<Document>(documentDto);
            _documentRepository.Update(document);
            await _documentRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null)
            {
                _documentRepository.Remove(document);
                await _documentRepository.SaveChangesAsync();
            }
        }
    }
} 