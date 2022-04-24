namespace JosephHungerman.Data.Models
{
    public enum PageType
    {
        Home,
        About,
        Resume,
        Contact,
        Blog,
        MessageSuccess
    }
    public class Quote : BaseEntity, IEntity
    {
        public PageType PageType { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string CitationUrl { get; set; }
    }
}
