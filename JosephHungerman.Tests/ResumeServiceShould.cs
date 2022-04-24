using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using JosephHungerman.Data.Models;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services;
using JosephHungerman.Tests.Models;
using Moq;
using Xunit;

namespace JosephHungerman.Tests;

public class ResumeServiceShould
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly ResumeService _sut;

    public ResumeServiceShould()
    {
        _sut = new(_unitOfWork.Object);
    }

    #region GetResumeDetailsAsync

    [Fact]
    public async void ReturnGeneralExceptionWhenGetResumeDetailsDataCallThrowsException()
    {
        var exception = new Exception();
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceExceptionResponse(exception);

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>()))
            .ThrowsAsync(exception);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetResumeDetailsDataCallReturnsNull()
    {
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceNotFoundExceptionResponse();
        List<Resume>? resumeList = null;

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>()))
            .ReturnsAsync(resumeList);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnNotFoundExceptionWhenGetResumeDetailsDataCallReturnsEmptyList()
    {
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceNotFoundExceptionResponse();
        List<Resume>? resumeList = new();

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>()))
            .ReturnsAsync(resumeList);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnSuccessfulResponseWhenGetResumeDetailsDataCallIsSuccessful()
    {
        Resume resume = (Resume)MockServiceResults.GetResumeSuccessResult();
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume);
        IList<Resume> resumes = new List<Resume> {resume};

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>())).ReturnsAsync(resumes);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }
    #endregion

    #region AddResume

    [Fact]
    public async void ReturnGeneralExceptionWhenAddThrowsException()
    {
        var exception = new Exception("FAILED ADD");
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceExceptionResponse(exception);
        var resume = (Resume) MockServiceResults.GetResumeSuccessResult();
       
        _unitOfWork.Setup(x => x.ResumeRepository.AddAsync(It.IsAny<Resume>())).ThrowsAsync(exception);

        var response = await _sut.AddResumeAsync(resume);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnDbExceptionWhenSaveIsUnsuccessful()
    {
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceDbExceptionResponse();
        var resume = (Resume)MockServiceResults.GetResumeSuccessResult();

        _unitOfWork.Setup(x => x.ResumeRepository.AddAsync(It.IsAny<Resume>())).ReturnsAsync(resume);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(false);

        var response = await _sut.AddResumeAsync(resume);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnSuccessfulResponseWhenSaveIsSuccessful()
    {
        var resume = (Resume)MockServiceResults.GetResumeSuccessResult();
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume);

        _unitOfWork.Setup(x => x.ResumeRepository.AddAsync(It.IsAny<Resume>())).ReturnsAsync(resume);
        _unitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(true);

        var response = await _sut.AddResumeAsync(resume);

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    #endregion
}