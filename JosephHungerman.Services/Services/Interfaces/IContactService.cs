using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Services.Interfaces
{
    public interface IContactService
    {
        Task<ResponseDto> GetMessagesAsync();
        Task<ResponseDto> AddMessageAsync(MessageDto message);
    }
}
