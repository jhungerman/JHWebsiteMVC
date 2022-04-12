using System.Text.Json;
using JosephHungerman.Core.Options;
using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using Microsoft.Extensions.Options;

namespace JosephHungerman.Services;

public class CaptchaService : ICaptchaService
{
    private readonly ILogger<CaptchaService> _logger;
    private readonly CaptchaSettings _captchaSettings;
    private bool _result;
    private const string GoogleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";

    public CaptchaService(IOptions<CaptchaSettings> captchaSettings, ILogger<CaptchaService> logger)
    {
        _logger = logger;
        _captchaSettings = captchaSettings.Value;
    }

    public string ClientKey => _captchaSettings.ClientKey;

    public async Task<bool> IsCaptchaValid(string token)
    {
        try
        {
            using var client = new HttpClient();

            var response =
                await client.PostAsync($"{GoogleVerificationUrl}?secret={_captchaSettings.ServerKey}&response={token}",
                    null);
            var jsonString = await response.Content.ReadAsStringAsync();
            var captchaVerification = JsonSerializer.Deserialize<CaptchaVerificationResponse>(jsonString,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            _result = captchaVerification!.Success;
        }
        catch (Exception e)
        {
            _logger.LogError("Captcha failed", e);
        }

        return _result;
    }
}