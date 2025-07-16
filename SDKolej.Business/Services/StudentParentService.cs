using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;
using System.Linq;

namespace SDKolej.Business.Services
{
    public class StudentParentService : IStudentParentService
    {
        private readonly IRepository<StudentParent> _studentParentRepository;
        private readonly IMapper _mapper;

        public StudentParentService(IRepository<StudentParent> studentParentRepository, IMapper mapper)
        {
            _studentParentRepository = studentParentRepository;
            _mapper = mapper;
        }

        public async Task<StudentParentDto> GetByIdAsync(int studentId, int parentId)
        {
            var studentParent = (await _studentParentRepository.FindAsync(sp => sp.StudentId == studentId && sp.ParentId == parentId)).FirstOrDefault();
            return _mapper.Map<StudentParentDto>(studentParent);
        }

        public async Task<IEnumerable<StudentParentDto>> GetAllAsync()
        {
            var studentParents = await _studentParentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentParentDto>>(studentParents);
        }

        public async Task AddAsync(StudentParentDto studentParentDto)
        {
            var studentParent = _mapper.Map<StudentParent>(studentParentDto);
            await _studentParentRepository.AddAsync(studentParent);
            await _studentParentRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentParentDto studentParentDto)
        {
            var studentParent = _mapper.Map<StudentParent>(studentParentDto);
            _studentParentRepository.Update(studentParent);
            await _studentParentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int studentId, int parentId)
        {
            var studentParent = (await _studentParentRepository.FindAsync(sp => sp.StudentId == studentId && sp.ParentId == parentId)).FirstOrDefault();
            if (studentParent != null)
            {
                _studentParentRepository.Remove(studentParent);
                await _studentParentRepository.SaveChangesAsync();
            }
        }
    }
} 