using JosephHungerman.Data.Models;

namespace JosephHungerman.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Message>? _messageRepository;
        private IRepository<Resume>? _resumeRepository;
        private IRepository<Quote>? _quoteRepository;
        private IRepository<Section>? _sectionRepository;
        private IRepository<Paragraph>? _paragraphRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<Message> MessageRepository => _messageRepository ??= new Repository<Message>(_context);
        public IRepository<Resume> ResumeRepository => _resumeRepository ??= new Repository<Resume>(_context);
        public IRepository<Quote> QuoteRepository => _quoteRepository ??= new Repository<Quote>(_context);
        public IRepository<Section> SectionRepository => _sectionRepository ??= new Repository<Section>(_context);
        public IRepository<Paragraph> ParagraphRepository => _paragraphRepository ??= new Repository<Paragraph>(_context);

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
