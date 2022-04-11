using System.ComponentModel.DataAnnotations;

namespace JosephHungerman.Models.Work
{
    public enum SkillType
    {
        Business,
        Personal,
        Technical
    }

    public class Skill : BaseEntity, IEntity
    {
        [Required] public string Name { get; set; }
        [Required] public SkillType SkillType { get; set; }
        public bool IsKeySkill { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
