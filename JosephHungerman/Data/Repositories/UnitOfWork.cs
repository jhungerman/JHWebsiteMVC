using JosephHungerman.Models;
using JosephHungerman.Models.Contact;

namespace JosephHungerman.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Message>? _messageRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Message> MessageRepository => _messageRepository ??= new Repository<Message>(_context);

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
