using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Services.Interfaces;

public interface IAboutService
{
    Task<ResponseDto> GetSectionsAsync();
    Task<ResponseDto> UpdateSectionsAsync(IList<Section> sections);
}