using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Identity.Models;

public class Email : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Address { get; set; }
}