using AutoMapper;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto> GetMessagesAsync()
        {
            var messages = await _unitOfWork.MessageRepository.GetAsync();

            if (messages == null || !messages!.Any())
            {
                return new ServiceResponseDtos<List<Message>>.ServiceNotFoundExceptionResponse();
            }

            var messagesDto = _mapper.Map<List<MessageDto>>(messages);
            return new ServiceResponseDtos<List<MessageDto>>.ServiceSuccessResponse(messagesDto);
        }

        public async Task<ResponseDto> AddMessageAsync(MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
