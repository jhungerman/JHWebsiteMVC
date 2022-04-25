using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
