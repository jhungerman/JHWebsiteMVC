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
            try
            {
                var messages = await _unitOfWork.MessageRepository.GetAsync();

                if (messages == null || !messages!.Any())
                {
                    return new ServiceResponseDtos<List<Message>>.ServiceNotFoundExceptionResponse();
                }

                return new ServiceResponseDtos<List<MessageDto>>.ServiceSuccessResponse(_mapper.Map<List<MessageDto>>(messages));
            }
            catch (Exception e)
            {
                return new ServiceResponseDtos<List<Message>>.ServiceExceptionResponse(e);
            }
        }

        public async Task<ResponseDto> AddMessageAsync(MessageDto message)
        {
            try
            {
                var newMessage = _mapper.Map<Message>(message);

                var result = await _unitOfWork.MessageRepository.AddAsync(newMessage);

                if (result == null)
                {
                    return new ServiceResponseDtos<Message>.ServiceDbExceptionResponse();
                }

                bool isSaveSuccessful = await _unitOfWork.SaveChangesAsync();

                if (!isSaveSuccessful)
                {
                    return new ServiceResponseDtos<Message>.ServiceDbExceptionResponse();
                }

                return new ServiceResponseDtos<MessageDto>.ServiceSuccessResponse(_mapper.Map<MessageDto>(result));
            }
            catch (Exception e)
            {
                return new ServiceResponseDtos<List<Message>>.ServiceExceptionResponse(e);
            }
        }
    }
}
