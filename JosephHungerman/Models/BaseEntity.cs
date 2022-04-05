using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
