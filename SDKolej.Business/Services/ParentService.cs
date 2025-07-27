using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class ParentService : IParentService
    {
        private readonly IRepository<Parent> _parentRepository;
        private readonly IMapper _mapper;
        
        public ParentService(IRepository<Parent> parentRepository, IMapper mapper)
        {
            _parentRepository = parentRepository ?? throw new ArgumentNullException(nameof(parentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<ParentDto?> GetByIdAsync(int id)
        {
            var parent = await _parentRepository.GetByIdAsync(id);
            return parent != null ? _mapper.Map<ParentDto>(parent) : null;
        }
        
        public async Task<IEnumerable<ParentDto>> GetAllAsync()
        {
            var parents = await _parentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);
        }
        
        public async Task AddAsync(ParentDto parentDto)
        {
            if (parentDto == null)
                throw new ArgumentNullException(nameof(parentDto));
                
            var parent = _mapper.Map<Parent>(parentDto);
            await _parentRepository.AddAsync(parent);
            await _parentRepository.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(ParentDto parentDto)
        {
            if (parentDto == null)
                throw new ArgumentNullException(nameof(parentDto));
                
            var parent = _mapper.Map<Parent>(parentDto);
            _parentRepository.Update(parent);
            await _parentRepository.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var parent = await _parentRepository.GetByIdAsync(id);
            if (parent != null)
            {
                _parentRepository.Remove(parent);
                await _parentRepository.SaveChangesAsync();
            }
        }
    }
} 