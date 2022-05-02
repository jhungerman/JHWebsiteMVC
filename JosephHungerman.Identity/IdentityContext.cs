using JosephHungerman.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Identity;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    public DbSet<Email> Emails { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Email>().HasData(
            new Email {Id = 1, Address = "joseph_hungerman@yahoo.com"},
            new Email {Id = 2, Address = "joseph.hungerman@gmail.com"},
            new Email {Id = 3, Address = "development@jacarutech.com"},
            new Email {Id = 4, Address = "ashlynashley@gmail.com"}
        );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
