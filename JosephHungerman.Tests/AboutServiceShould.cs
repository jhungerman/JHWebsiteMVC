using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.About;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services;
using JosephHungerman.Tests.Models;
using Moq;
using Xunit;

namespace JosephHungerman.Tests;

public class AboutServiceShould
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly AboutService _sut;

    public AboutServiceShould()
    {
        _sut = new(_unitOfWork.Object);
    }

    #region GetSections

    [Fact]
    public async void ReturnGeneralExceptionWhenGetAsyncThrowsException()
    {
        var exception = new Exception("GET FAILED");
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceExceptionResponse(exception);

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ThrowsAsync(exception);

        var response = await _sut.GetSectionsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetAsyncReturnsNull()
    {
        List<Section>? sections = null;
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(sections);

        var response = await _sut.GetSectionsAsync();

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetAsyncReturnsEmptyList()
    {
        List<Section> sections = new();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(sections);

        var response = await _sut.GetSectionsAsync();

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnSuccessfulResponseWhenGetAsyncIsSuccessful()
    {
        List<Section> sections = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceSuccessResponse(sections);

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(sections);

        var response = await _sut.GetSectionsAsync();

        response.Should().BeEquivalentTo(expectedResponse);
    }


    #endregion

}