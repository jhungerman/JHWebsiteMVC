using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Services.Interfaces;

public interface IQuoteService
{
    Task<ResponseDto> GetPageQuoteAsync(PageType pageType);
    Task<ResponseDto> UpdateQuoteAsync(Quote resumeModelQuote);
    Task<ResponseDto> GetPageQuotesAsync();
    Task<ResponseDto> UpdateQuotesAsync(IList<Quote> quotesViewQuotes);
}