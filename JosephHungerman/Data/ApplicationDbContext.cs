using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Models;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Message> Messages { get; set; }
    }
}
