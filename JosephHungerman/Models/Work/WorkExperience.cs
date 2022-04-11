using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JosephHungerman.Models.Work
{
    public class WorkExperience : BaseEntity, IEntity
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCity { get; set; }
        [Required]
        public string CompanyState { get; set; }
        public string? CompanyUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IList<WorkDetail> WorkDetails { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
