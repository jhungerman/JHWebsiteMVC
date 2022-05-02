using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Identity.Models
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
