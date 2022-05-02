using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseDto> SendEmailAsync(MessageDto message);
        Task<ResponseDto> SendHtmlEmailAsync(MessageDto message);
    }
}
