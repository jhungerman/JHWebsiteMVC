using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services.Interfaces;

public interface IQuoteService
{
    Task<ResponseDto> GetPageQuoteAsync(PageType pageType);
}