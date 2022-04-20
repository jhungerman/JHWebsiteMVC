using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services;
using JosephHungerman.Tests.Models;
using Moq;
using Xunit;

namespace JosephHungerman.Tests;

public class QuoteServiceShould
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly QuoteService _sut;

    public QuoteServiceShould()
    {
        _sut = new(_unitOfWork.Object);
    }

    #region GetPageQuote

    [Fact]
    public async void ReturnGeneralExceptionWhenGetThrowsException()
    {
        var exception = new Exception("FAILED GET");
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceExceptionResponse(exception);

        _unitOfWork.Setup(x => x.QuoteRepository.GetAsync(It.IsAny<Expression<Func<Quote, bool>>?>(),
                It.IsAny<Func<IQueryable<Quote>, IOrderedQueryable<Quote>>?>(), It.IsAny<string>()))
            .ThrowsAsync(exception);

        var response = await _sut.GetPageQuoteAsync(It.IsAny<PageType>());

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetReturnsNull()
    {
        List<Quote>? quotes = null;
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.QuoteRepository.GetAsync(It.IsAny<Expression<Func<Quote, bool>>?>(),
                It.IsAny<Func<IQueryable<Quote>, IOrderedQueryable<Quote>>?>(), It.IsAny<string>()))
            .ReturnsAsync(quotes);

        var response = await _sut.GetPageQuoteAsync(It.IsAny<PageType>());

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetReturnsEmptyList()
    {
        List<Quote> quotes = new();
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.QuoteRepository.GetAsync(It.IsAny<Expression<Func<Quote, bool>>?>(),
                It.IsAny<Func<IQueryable<Quote>, IOrderedQueryable<Quote>>?>(), It.IsAny<string>()))
            .ReturnsAsync(quotes);

        var response = await _sut.GetPageQuoteAsync(It.IsAny<PageType>());

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnSuccessfulResponseWhenGetIsSuccessful()
    {
        List<Quote> quotes = new()
        {
            (Quote) MockServiceResults.GetQuoteSuccessResult()
        };
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceSuccessResponse(quotes.First());

        _unitOfWork.Setup(x => x.QuoteRepository.GetAsync(It.IsAny<Expression<Func<Quote, bool>>?>(),
                It.IsAny<Func<IQueryable<Quote>, IOrderedQueryable<Quote>>?>(), It.IsAny<string>()))
            .ReturnsAsync(quotes);

        var response = await _sut.GetPageQuoteAsync(PageType.Home);

        response.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion

    #region UpdateQuote

    [Fact]
    public async void ReturnGeneralExceptionWhenUpdateThrowsException()
    {
        var exception = new Exception("FAILED UPDATE");
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceExceptionResponse(exception);
        var quote = (Quote)MockServiceResults.GetQuoteSuccessResult();

        _unitOfWork.Setup(x => x.QuoteRepository.UpdateAsync(It.IsAny<Quote>()))
            .ThrowsAsync(exception);

        var response = await _sut.UpdateQuoteAsync(quote);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnDbExceptionWhenSaveIsUnsuccessful()
    {
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceDbExceptionResponse();
        var quote = (Quote)MockServiceResults.GetQuoteSuccessResult();

        _unitOfWork.Setup(x => x.QuoteRepository.UpdateAsync(It.IsAny<Quote>())).ReturnsAsync(quote);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);

        var response = await _sut.UpdateQuoteAsync(quote);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnSuccessfulResponseWhenSaveIsSuccessful()
    {
        var quote = (Quote)MockServiceResults.GetQuoteSuccessResult();
        var expectedResponse = new ServiceResponseDtos<Quote>.ServiceSuccessResponse(quote);

        _unitOfWork.Setup(x => x.QuoteRepository.UpdateAsync(It.IsAny<Quote>())).ReturnsAsync(quote);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

        var response = await _sut.UpdateQuoteAsync(quote);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    #endregion
}