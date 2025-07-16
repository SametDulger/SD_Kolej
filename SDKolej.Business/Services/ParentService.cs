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
            _parentRepository = parentRepository;
            _mapper = mapper;
        }
        public async Task<ParentDto> GetByIdAsync(int id)
        {
            var parent = await _parentRepository.GetByIdAsync(id);
            return _mapper.Map<ParentDto>(parent);
        }
        public async Task<IEnumerable<ParentDto>> GetAllAsync()
        {
            var parents = await _parentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParentDto>>(parents);
        }
        public async Task AddAsync(ParentDto parentDto)
        {
            var parent = _mapper.Map<Parent>(parentDto);
            await _parentRepository.AddAsync(parent);
            await _parentRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(ParentDto parentDto)
        {
            var parent = _mapper.Map<Parent>(parentDto);
            _parentRepository.Update(parent);
            await _parentRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var parent = await _parentRepository.GetByIdAsync(id);
            _parentRepository.Remove(parent);
            await _parentRepository.SaveChangesAsync();
        }
    }
} 