using JosephHungerman.Identity.Models;

namespace JosephHungerman.Identity.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _context;
        private IRepository<Email>? _whitelistRepository;

        public UnitOfWork(IdentityContext context)
        {
            _context = context;
            context.Database.EnsureCreatedAsync().GetAwaiter();
        }

        public IRepository<Email> WhitelistRepository => _whitelistRepository ??= new Repository<Email>(_context);

        public async Task<bool> SaveChangesAsync()
        {
            if (_context.ChangeTracker.HasChanges())
            {
                return await _context.SaveChangesAsync() > 0;
            }

            return true;
        }
    }
}
