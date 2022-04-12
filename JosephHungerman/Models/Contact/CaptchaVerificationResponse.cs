using System.Text.Json.Serialization;

namespace JosephHungerman.Models.Contact
{
    public class CaptchaVerificationResponse
    {
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
        public string Hostname { get; set; }
        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
