using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JosephHungerman.Models.Work
{
    public class Resume : BaseEntity, IEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        [ForeignKey("WorkExperienceId")]
        IList<WorkExperience> WorkExperiences { get; set; }
        [Required]
        [ForeignKey("EducationId")]
        IList<Education> Educations { get; set; }
        [Required]
        [ForeignKey("SkillId")]
        IList<Skill> Skills { get; set; }
        [Required]
        [ForeignKey("CertificationId")]
        IList<Certification> Certifications { get; set; }
    }
}
