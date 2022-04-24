using JosephHungerman.Data.Models;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services.Interfaces;

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

    public async Task<ResponseDto> UpdateQuoteAsync(Quote resumeModelQuote)
    {
        try
        {
            var response = await _unitOfWork.QuoteRepository.UpdateAsync(resumeModelQuote);

            var saveSuccessful = await _unitOfWork.SaveChangesAsync();

            if (saveSuccessful)
            {
                return new ServiceResponseDtos<Quote>.ServiceSuccessResponse(response);
            }

            return new ServiceResponseDtos<Quote>.ServiceDbExceptionResponse();
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Quote>.ServiceExceptionResponse(e);
        }
    }

    public async Task<ResponseDto> GetPageQuotesAsync()
    {
        try
        {
            var quotes = await _unitOfWork.QuoteRepository.GetAsync();

            if (quotes != null && quotes.Any())
            {
                return new ServiceResponseDtos<List<Quote>>.ServiceSuccessResponse(quotes.ToList());
            }

            return new ServiceResponseDtos<List<Quote>>.ServiceNotFoundExceptionResponse();
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<List<Quote>>.ServiceExceptionResponse(e);
        }
    }

    public async Task<ResponseDto> UpdateQuotesAsync(IList<Quote> quotesViewQuotes)
    {
        try
        {
            var response = await _unitOfWork.QuoteRepository.UpdateAllAsync(quotesViewQuotes);

            var saveSuccessful = await _unitOfWork.SaveChangesAsync();

            if (saveSuccessful)
            {
                return new ServiceResponseDtos<List<Quote>>.ServiceSuccessResponse(response.ToList());
            }

            return new ServiceResponseDtos<List<Quote>>.ServiceDbExceptionResponse();
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<List<Quote>>.ServiceExceptionResponse(e);
        }
    }
}