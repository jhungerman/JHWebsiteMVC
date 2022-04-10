using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Contact
{
    public class Message : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }
        public string Detail { get; set; }
    }
}
