using JosephHungerman.Data.Models;

namespace JosephHungerman.UI.Models.ViewModels
{
    public class ContactViewModel
    {
        public Quote Quote { get; set; }
        public string CaptchaClientKey { get; set; }
        public Message Message { get; set; }
    }
}
