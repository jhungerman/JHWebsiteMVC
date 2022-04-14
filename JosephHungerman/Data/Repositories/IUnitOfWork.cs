using JosephHungerman.Models;
using JosephHungerman.Models.About;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        IRepository<Resume> ResumeRepository { get; }
        IRepository<Quote> QuoteRepository { get; }
        IRepository<Section> SectionRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
