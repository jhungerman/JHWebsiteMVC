using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using JosephHungerman.Models;
using JosephHungerman.Models.Contact;
using JosephHungerman.Models.Work;
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
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<WorkDetail> WorkDetails { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Quote> Quotes { get; set; }
    }
}
