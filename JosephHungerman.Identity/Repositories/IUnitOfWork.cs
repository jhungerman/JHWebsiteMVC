using JosephHungerman.Identity.Models;

namespace JosephHungerman.Identity.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Email> WhitelistRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
