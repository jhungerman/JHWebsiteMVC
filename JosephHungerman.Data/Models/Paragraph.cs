namespace JosephHungerman.Data.Models
{
    public class Paragraph : BaseEntity
    {
        public string Content { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}
