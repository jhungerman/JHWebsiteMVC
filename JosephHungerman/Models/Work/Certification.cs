using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Work
{
    public class Certification : BaseEntity, IEntity
    {
        [Required] public string Source { get; set; }
        public string? SourceUrl { get; set; }
        [Required] public string Subject { get; set; }
        public string? CredentialId { get; set; }
        [Required] public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
