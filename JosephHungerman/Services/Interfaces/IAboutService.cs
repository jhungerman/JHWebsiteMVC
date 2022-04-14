using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services.Interfaces;

public interface IAboutService
{
    Task<ResponseDto> GetSectionsAsync();
}