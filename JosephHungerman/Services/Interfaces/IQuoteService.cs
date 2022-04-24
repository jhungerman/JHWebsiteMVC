using JosephHungerman.Data.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services.Interfaces;

public interface IQuoteService
{
    Task<ResponseDto> GetPageQuoteAsync(PageType pageType);
    Task<ResponseDto> UpdateQuoteAsync(Quote resumeModelQuote);
    Task<ResponseDto> GetPageQuotesAsync();
    Task<ResponseDto> UpdateQuotesAsync(IList<Quote> quotesViewQuotes);
}