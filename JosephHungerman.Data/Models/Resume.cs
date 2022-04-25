using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class Resume : BaseEntity
    {
        [Required] 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string Name { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public IList<WorkExperience> WorkExperiences { get; set; }
        [Required]
        public IList<Education> Educations { get; set; }
        [Required]
        public IList<Skill> Skills { get; set; }
        [Required]
        public IList<Certification> Certifications { get; set; }
    }
}
