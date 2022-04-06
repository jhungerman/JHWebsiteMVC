using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Dtos
{
    public class MessageDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Detail { get; set; }
    }
}
