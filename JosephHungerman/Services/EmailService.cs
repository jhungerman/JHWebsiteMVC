using JosephHungerman.Core.Options;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Dtos.Contact;
using JosephHungerman.Services.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using MailSettings = JosephHungerman.Core.Options.MailSettings;

namespace JosephHungerman.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _client;
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings, ISendGridClient client)
        {
            _client = client;
            _mailSettings = mailSettings.Value;
        }

        public async Task<ResponseDto> SendEmailAsync(MessageDto message)
        {
            try
            {
                var from = new EmailAddress(_mailSettings.Mail);
                var to = new EmailAddress(_mailSettings.ToMail);
                var subject = "You have a new contact request from JosephHungerman.com";
                var textContent =
                    $@"{message.FirstName}{Environment.NewLine}{message.LastName}{Environment.NewLine}{message.Email}{Environment.NewLine}{message.Subject}{Environment.NewLine}{message.Detail}";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, textContent, "");
                var response = await _client.SendEmailAsync(msg);

                var deserialized = await response.DeserializeResponseBodyAsync();

                if (response is { IsSuccessStatusCode: true })
                {
                    return new ServiceResponseDtos<Dictionary<string, dynamic>>.ServiceSuccessResponse(deserialized);
                }

                return new ServiceResponseDtos<SendGridMessage>.ServiceBadRequestExceptionResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponseDtos<SendGridMessage>.ServiceExceptionResponse(e);
            }
        }
    }
}
