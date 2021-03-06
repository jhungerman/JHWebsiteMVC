namespace JosephHungerman.Services.Services.Interfaces;

public interface ICaptchaService
{
    public string ClientKey { get; }
    Task<bool> IsCaptchaValid(string token);
}