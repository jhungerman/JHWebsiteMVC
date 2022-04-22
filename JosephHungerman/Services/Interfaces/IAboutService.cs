using JosephHungerman.Models.About;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.ViewModels;

namespace JosephHungerman.Services.Interfaces;

public interface IAboutService
{
    Task<ResponseDto> GetSectionsAsync();
    Task<ResponseDto> UpdateSectionsAsync(IList<Section> sections);
}