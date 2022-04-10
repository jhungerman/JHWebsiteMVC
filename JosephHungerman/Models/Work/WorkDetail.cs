using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Work
{
    public class WorkDetail : BaseEntity, IEntity
    {
        [Required]
        public string Detail { get; set; }
    }
}
