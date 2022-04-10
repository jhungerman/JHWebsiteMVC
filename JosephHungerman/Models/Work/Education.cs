using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Work
{
    public class Education : BaseEntity, IEntity
    {
        [Required]
        public string InstitutionName { get; set; }
        [Required] public string Credential { get; set; }
        [Required] public DateTime EndDate { get; set; }
        public string? InstitutionUrl { get; set; }
    }
}
