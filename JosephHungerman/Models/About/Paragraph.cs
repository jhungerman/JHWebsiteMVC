namespace JosephHungerman.Models.About
{
    public class Paragraph : BaseEntity, IEntity
    {
        public string Content { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
