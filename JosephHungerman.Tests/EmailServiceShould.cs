using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using FluentAssertions;
using JosephHungerman.Core.Options;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;
using JosephHungerman.Services;
using JosephHungerman.Services.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using Xunit;
using MailSettings = JosephHungerman.Core.Options.MailSettings;

namespace JosephHungerman.Tests
{
    public class EmailServiceShould
    {
        private readonly SendGridMessage _message;
        private readonly IOptions<MailSettings> _mailSettings;
        private readonly IOptions<EmailSettings> _emailOptions;
        private readonly MessageDto _messageDto;
        private readonly Mock<ISendGridClient> _client = new();
        private readonly Mock<Func<Response>> _response = new();
        private readonly IEmailService _sut;

        public EmailServiceShould()
        {
            _message = MailHelper.CreateSingleEmail(new("joe@joe.com"), new("ash@ash.com"),
                "Hi", "Test", "");
            _mailSettings = Options.Create(new MailSettings
            {
                Mail = _message.From.ToString()!,
                ToMail = "test@gmail.com",
                Host = "smtp@gmail.com",
                Port = 587
            });
            _emailOptions = Options.Create(new EmailSettings
            {
                Username = "Joe",
                Password = "Mypassword",
                ApiKey = "thisisanapikey"
            });
            _messageDto = new()
            {
                FirstName = "Joe",
                LastName = "H",
                Email = "Joe@Joe.com",
                Subject = "Hi",
                Detail = "This is a unit test."
            };
            _sut = new EmailService(_mailSettings, _client.Object);
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
