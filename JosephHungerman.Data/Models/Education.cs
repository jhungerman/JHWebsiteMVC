using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class Education : BaseEntity
    {
        [Required]
        public string InstitutionName { get; set; }
        [Required] public string Credential { get; set; }
        [Required] public DateTime EndDate { get; set; } = DateTime.Today;
        public string? InstitutionUrl { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
