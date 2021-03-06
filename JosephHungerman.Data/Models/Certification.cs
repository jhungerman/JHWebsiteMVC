using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class Certification : BaseEntity
    {
        [Required] public string Source { get; set; }
        public string? SourceUrl { get; set; }
        [Required] public string Subject { get; set; }
        public string? CredentialId { get; set; }
        [Required] public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime? EndDate { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
