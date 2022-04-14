using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;

namespace JosephHungerman.Services.Interfaces
{
    public interface IContactService
    {
        Task<ResponseDto> GetMessagesAsync();
        Task<ResponseDto> AddMessageAsync(MessageDto message);
    }
}
