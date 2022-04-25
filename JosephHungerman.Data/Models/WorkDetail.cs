using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class WorkDetail : BaseEntity
    {
        [Required]
        public string Detail { get; set; }

        public int WorkExperienceId { get; set; }
        public WorkExperience WorkExperience { get; set; }
    }
}
