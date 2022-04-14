using JosephHungerman.Data.Repositories;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services;

public class QuoteService : IQuoteService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuoteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> GetPageQuoteAsync(PageType pageType)
    {
        try
        {
            var quotes = await _unitOfWork.QuoteRepository.GetAsync(q => q.PageType == pageType);

            if (quotes != null && quotes.Any())
            {
                return new ServiceResponseDtos<Quote>.ServiceSuccessResponse(quotes.First());
            }

            return new ServiceResponseDtos<Quote>.ServiceNotFoundExceptionResponse();
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Quote>.ServiceExceptionResponse(e);
        }
    }
}