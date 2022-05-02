using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JosephHungerman.Identity.Services;

public class ApplicationEmailSender : IEmailSender
{
    private readonly ISendGridClient _client;
    private readonly IConfiguration _configuration;

    public ApplicationEmailSender(ISendGridClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_configuration.GetSection("MailSettings:Mail").Value),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));

            message.SetClickTracking(false, false);

            await _client.SendEmailAsync(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}