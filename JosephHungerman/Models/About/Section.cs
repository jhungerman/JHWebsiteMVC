namespace JosephHungerman.Models.About
{
    public class Section : BaseEntity, IEntity
    {
        public int OrderIndex { get; set; }
        public string Title { get; set; }
        public IList<Paragraph> Paragraphs { get; set; }
    }
}
