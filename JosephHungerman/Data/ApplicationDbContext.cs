using JosephHungerman.Models;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
    }
}
