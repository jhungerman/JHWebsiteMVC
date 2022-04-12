using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        IRepository<Resume> ResumeRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
