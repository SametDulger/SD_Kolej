using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly IMapper _mapper;
        public AnnouncementService(IRepository<Announcement> announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }
        public async Task<AnnouncementDto> GetByIdAsync(int id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);
            return _mapper.Map<AnnouncementDto>(announcement);
        }
        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnnouncementDto>>(announcements);
        }
        public async Task AddAsync(AnnouncementDto announcementDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementDto);
            await _announcementRepository.AddAsync(announcement);
            await _announcementRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(AnnouncementDto announcementDto)
        {
            var announcement = _mapper.Map<Announcement>(announcementDto);
            _announcementRepository.Update(announcement);
            await _announcementRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);
            _announcementRepository.Remove(announcement);
            await _announcementRepository.SaveChangesAsync();
        }
    }
} 