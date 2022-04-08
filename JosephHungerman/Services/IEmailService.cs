using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services
{
    public interface IEmailService
    {
        Task<ResponseDto> SendEmailAsync(MessageDto message);
    }
}
