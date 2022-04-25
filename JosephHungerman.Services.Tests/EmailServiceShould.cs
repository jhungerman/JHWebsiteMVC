using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using FluentAssertions;
using JosephHungerman.Services.Models.Dtos;
using JosephHungerman.Services.Services;
using JosephHungerman.Services.Services.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Xunit;
using MailSettings = JosephHungerman.Services.Settings.MailSettings;

namespace JosephHungerman.Services.Tests
{
    public class EmailServiceShould
    {
        private readonly MessageDto _messageDto;
        private readonly Mock<ISendGridClient> _client = new();
        private readonly Mock<Func<Response>> _response = new();
        private readonly IEmailService _sut;

        public EmailServiceShould()
        {
            var message = MailHelper.CreateSingleEmail(new("joe@joe.com"), new("ash@ash.com"),
                "Hi", "Test", "");
            var mailSettings = Options.Create(new MailSettings
            {
                Mail = message.From.ToString()!,
                ToMail = "test@gmail.com",
                Host = "smtp@gmail.com",
                Port = 587
            });
            _messageDto = new()
            {
                FirstName = "Joe",
                LastName = "H",
                Email = "Joe@Joe.com",
                Subject = "Hi",
                Detail = "This is a unit test."
            };
            _sut = new EmailService(mailSettings, _client.Object);
        }

        [Fact]
        public async void ReturnGeneralExceptionWhenSendGridThrowsException()
        {
            var exception = new Exception("FAILURE");
            var expectedResponse = new ServiceResponseDtos<SendGridMessage>.ServiceExceptionResponse(exception);

            _response.Setup(x => x.Invoke()).Throws(exception);
            _client.Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>())).ThrowsAsync(exception);

            var response = await _sut.SendEmailAsync(_messageDto);

            response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
        }

        [Fact]
        public async void ReturnBadRequestExceptionExceptionWhenSendGridIsUnsuccessful()
        {
            var message = new HttpResponseMessage();
            var httpHeaders = message.Headers;
            var sendGridResponse =
                new Response(HttpStatusCode.Unauthorized, new ByteArrayContent(Array.Empty<byte>()), httpHeaders);
            _client.Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>())).ReturnsAsync(sendGridResponse);
            var expectedResponse = new ServiceResponseDtos<Dictionary<string, dynamic>>.ServiceBadRequestExceptionResponse();

            var response = await _sut.SendEmailAsync(_messageDto);

            response.Should().BeEquivalentTo(expectedResponse, opt => opt.Excluding(o => o.ErrorMessages));
        }

        [Fact]
        public async void ReturnSuccessfulResponseWhenSendGridMessageIsSuccessful()
        {
            var message = new HttpResponseMessage();
            var httpHeaders = message.Headers;
            var sendGridResponse =
                new Response(HttpStatusCode.OK, new ByteArrayContent(Array.Empty<byte>()), httpHeaders);
            _client.Setup(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>())).ReturnsAsync(sendGridResponse);
            var deserialized = await sendGridResponse.DeserializeResponseBodyAsync();
            var expectedResponse = new ServiceResponseDtos<Dictionary<string, dynamic>>.ServiceSuccessResponse(deserialized);

            var response = await _sut.SendEmailAsync(_messageDto);

            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
