using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Data.Models
{
    public enum SkillType
    {
        Business,
        Personal,
        Technical
    }

    public class Skill : BaseEntity
    {
        [Required] public string Name { get; set; }
        [Required] public SkillType SkillType { get; set; }
        public bool IsKeySkill { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
