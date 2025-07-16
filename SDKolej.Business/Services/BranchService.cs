using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class BranchService : IBranchService
    {
        private readonly IRepository<Branch> _branchRepository;
        private readonly IMapper _mapper;

        public BranchService(IRepository<Branch> branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchDto> GetByIdAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            return _mapper.Map<BranchDto>(branch);
        }

        public async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            var branches = await _branchRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchDto>>(branches);
        }

        public async Task AddAsync(BranchDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto);
            await _branchRepository.AddAsync(branch);
            await _branchRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(BranchDto branchDto)
        {
            var branch = _mapper.Map<Branch>(branchDto);
            _branchRepository.Update(branch);
            await _branchRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            _branchRepository.Remove(branch);
            await _branchRepository.SaveChangesAsync();
        }
    }
} 