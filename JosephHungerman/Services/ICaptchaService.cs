namespace JosephHungerman.Services;

public interface ICaptchaService
{
    public string ClientKey { get; }
    Task<bool> IsCaptchaValid(string token);
}