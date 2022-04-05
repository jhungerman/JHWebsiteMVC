using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models
{
    public class Message
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }
        public string Detail { get; set; }
    }
}
