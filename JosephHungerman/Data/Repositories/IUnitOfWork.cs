using JosephHungerman.Models;
using JosephHungerman.Models.Contact;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
