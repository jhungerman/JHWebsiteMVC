using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Work;
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
        _sut = new ResumeService(_unitOfWork.Object);
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
        List<Resume>? resumeList = new List<Resume>();

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>()))
            .ReturnsAsync(resumeList);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }

    [Fact]
    public async void ReturnSuccssfulResponseWhenGetResumeDetailsDataCallIsSuccessful()
    {
        Resume resume = (Resume)MockServiceResults.GetResumeSuccessResult();
        var expectedResponse = new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume);
        List<Resume> resumeList = new List<Resume> {resume};

        _unitOfWork.Setup(x => x.ResumeRepository.GetAsync(It.IsAny<Expression<Func<Resume, bool>>?>(),
                It.IsAny<Func<IQueryable<Resume>, IOrderedQueryable<Resume>>?>(), It.IsAny<string>()))
            .ReturnsAsync(resumeList);

        var response = await _sut.GetResumeDetailsAsync();

        response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
    }
    #endregion
}