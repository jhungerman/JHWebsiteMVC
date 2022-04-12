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
        public IList<WorkExperience> WorkExperiences { get; set; }
        [Required]
        public IList<Education> Educations { get; set; }
        [Required]
        public IList<Skill> Skills { get; set; }
        [Required]
        public IList<Certification> Certifications { get; set; }
    }
}
