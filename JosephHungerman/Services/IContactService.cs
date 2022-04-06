using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services
{
    public interface IContactService
    {
        Task<ResponseDto> GetMessagesAsync();
        Task<ResponseDto> AddMessageAsync(MessageDto message);
    }
}
