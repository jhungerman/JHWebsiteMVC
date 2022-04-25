using JosephHungerman.Data.Models;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        IRepository<Resume> ResumeRepository { get; }
        IRepository<Quote> QuoteRepository { get; }
        IRepository<Section> SectionRepository { get; }
        IRepository<Paragraph> ParagraphRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
