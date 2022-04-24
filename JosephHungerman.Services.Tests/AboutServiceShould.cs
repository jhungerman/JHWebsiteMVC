using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using JosephHungerman.Data.Models;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Services.Models.Dtos;
using JosephHungerman.Services.Services;
using JosephHungerman.Services.Tests.Models;
using Moq;
using Xunit;

namespace JosephHungerman.Services.Tests;

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

    #region UpdateSections

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetReturnsNull()
    {
        List<Section>? sections = null;
        var setupSections = (List<Section>) MockServiceResults.GetSectionsSuccessResult();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(sections);

        var response = await _sut.UpdateSectionsAsync(setupSections);

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetReturnsEmptyList()
    {
        List<Section> sections = new();
        var setupSections = (List<Section>) MockServiceResults.GetSectionsSuccessResult();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(sections);

        var response = await _sut.UpdateSectionsAsync(setupSections);

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnGeneralExceptionWhenGetThrowsException()
    {
        var exception = new Exception("FAILED GET");
        var setupSections = (List<Section>) MockServiceResults.GetSectionsSuccessResult();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceExceptionResponse(exception);

        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ThrowsAsync(exception);

        var response = await _sut.UpdateSectionsAsync(setupSections);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void CallDeleteSectionIfUpdateListIsMissingSection()
    {
        var setupSections = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var updateList = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(setupSections);


        updateList.RemoveAt(0);

        await _sut.UpdateSectionsAsync(updateList);

        _unitOfWork.Verify(x => x.SectionRepository.DeleteAsync(It.IsAny<Section>()), Times.Once);
    }

    [Fact]
    public async void CallDeleteParagraphIfUpdateListIsMissingParagraph()
    {
        var setupSections = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var updateList = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(setupSections);

        _unitOfWork.Setup(x => x.ParagraphRepository.DeleteAsync(It.IsAny<Paragraph>())).Returns(Task.CompletedTask);

        updateList[0].Paragraphs.RemoveAt(0);

        await _sut.UpdateSectionsAsync(updateList);

        _unitOfWork.Verify(x => x.ParagraphRepository.DeleteAsync(It.IsAny<Paragraph>()), Times.Once);
    }

    [Fact]
    public async void ReturnDbExceptionIfSaveIsUnsuccessful()
    {
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceDbExceptionResponse();
        var setupSections = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var updateList = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(setupSections);

        _unitOfWork.Setup(x => x.ParagraphRepository.DeleteAsync(It.IsAny<Paragraph>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);

        updateList[0].Paragraphs.RemoveAt(0);

        var response = await _sut.UpdateSectionsAsync(updateList);

        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async void ReturnSuccessfulResponseIfSaveIsSuccessful()
    {
        var setupSections = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var updateList = (List<Section>)MockServiceResults.GetSectionsSuccessResult();
        var expectedResponse = new ServiceResponseDtos<List<Section>>.ServiceSuccessResponse(updateList);
        _unitOfWork.Setup(x => x.SectionRepository.GetAsync(It.IsAny<Expression<Func<Section, bool>>?>(),
                It.IsAny<Func<IQueryable<Section>, IOrderedQueryable<Section>>?>(), It.IsAny<string>()))
            .ReturnsAsync(setupSections.OrderBy(s => s.OrderIndex).ToList);

        _unitOfWork.SetupSequence(x => x.SectionRepository.UpdateAsync(It.IsAny<Section>()))
            .ReturnsAsync(updateList[0])
            .ReturnsAsync(updateList[1]);
        _unitOfWork.Setup(x => x.ParagraphRepository.DeleteAsync(It.IsAny<Paragraph>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

        updateList[0].Paragraphs.RemoveAt(0);

        var response = await _sut.UpdateSectionsAsync(updateList);

        response.Should().BeEquivalentTo(expectedResponse);
    }

    #endregion
}