using System.Text.Json;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Data
{
    public class DbInitializer
    {
        public static async Task SeedResumeData(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Resumes.Any())
            {
                using StreamReader reader = new(@"Data\resumeseed.json");
                string resumeJson = await reader.ReadToEndAsync();
                Resume resume = JsonSerializer.Deserialize<Resume>(resumeJson);

                await context.AddAsync(resume);
                await context.SaveChangesAsync();
            }
        }
    }
}
