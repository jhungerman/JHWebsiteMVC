using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Models;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services;
using JosephHungerman.Tests.Models;
using Moq;
using Xunit;

namespace JosephHungerman.Tests
{
    public class ContactServiceShould
    {
        private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        public ContactServiceShould()
        {
        }

        #region GetMessages

        [Fact]
        public async void ReturnNotFoundResponseWhenRepositoryCallIsNull()
        {
            var expectedResponse = new ServiceResponseDtos<List<Message>>.ServiceNotFoundExceptionResponse();
            _unitOfWork.Setup(x => x.MessageRepository.GetAsync(It.IsAny<Expression<Func<Message, bool>>?>(),
                It.IsAny<Func<IQueryable<Message>, IOrderedQueryable<Message>>?>(), It.IsAny<string>())).ReturnsAsync((List<Message>) null);

            var sut = new ContactService(_unitOfWork.Object, _mapper.Object);
            var result = await sut.GetMessagesAsync();

            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void ReturnNotFoundResponseWhenRepositoryCallIsEmpty()
        {
            var expectedResponse = new ServiceResponseDtos<List<Message>>.ServiceNotFoundExceptionResponse();
            _unitOfWork.Setup(x => x.MessageRepository.GetAsync(It.IsAny<Expression<Func<Message, bool>>?>(),
                It.IsAny<Func<IQueryable<Message>, IOrderedQueryable<Message>>?>(), It.IsAny<string>())).ReturnsAsync(new List<Message>());

            var sut = new ContactService(_unitOfWork.Object, _mapper.Object);
            var result = await sut.GetMessagesAsync();

            result.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async void ReturnSuccessfulResponseWhenRepositoryCallIsSuccessful()
        {
            var successResponse = (List<Message>)MockServiceResults.GetMessagesSuccessResult();
            var mappedResponse = _mapper.Object.Map<List<MessageDto>>(successResponse);
            var expectedResponse = new ServiceResponseDtos<List<MessageDto>>.ServiceSuccessResponse(mappedResponse);
            _unitOfWork.Setup(x => x.MessageRepository.GetAsync(It.IsAny<Expression<Func<Message, bool>>?>(),
                It.IsAny<Func<IQueryable<Message>, IOrderedQueryable<Message>>?>(), It.IsAny<string>())).ReturnsAsync(successResponse);

            var sut = new ContactService(_unitOfWork.Object, _mapper.Object);
            var result = await sut.GetMessagesAsync();

            result.Should().BeEquivalentTo(expectedResponse);
        }
        #endregion
    }
}
