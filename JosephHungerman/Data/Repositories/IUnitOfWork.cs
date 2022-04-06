using JosephHungerman.Models;

namespace JosephHungerman.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Message> MessageRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
