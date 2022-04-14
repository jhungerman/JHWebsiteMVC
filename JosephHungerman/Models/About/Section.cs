namespace JosephHungerman.Models.About
{
    public class Section : BaseEntity, IEntity
    {
        public string Title { get; set; }
        public IList<Paragraph> Paragraphs { get; set; }
    }
}
