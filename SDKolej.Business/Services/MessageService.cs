using System.Collections.Generic;
using System.Threading.Tasks;
using SDKolej.Business.DTOs;
using SDKolej.Business.Interfaces;
using SDKolej.Data.Entities;
using SDKolej.Data.Repositories;
using AutoMapper;

namespace SDKolej.Business.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _messageRepository;
        private readonly IMapper _mapper;
        public MessageService(IRepository<Message> messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task<MessageDto> GetByIdAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            return _mapper.Map<MessageDto>(message);
        }
        public async Task<IEnumerable<MessageDto>> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
        public async Task AddAsync(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(MessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            _messageRepository.Update(message);
            await _messageRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            _messageRepository.Remove(message);
            await _messageRepository.SaveChangesAsync();
        }
    }
} 