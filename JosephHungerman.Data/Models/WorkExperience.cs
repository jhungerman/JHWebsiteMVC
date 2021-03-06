using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class WorkExperience : BaseEntity
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCity { get; set; }
        [Required]
        public string CompanyState { get; set; }
        public string? CompanyUrl { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IList<WorkDetail> WorkDetails { get; set; }

        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
