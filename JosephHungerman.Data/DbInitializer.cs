using System.Reflection;
using System.Text.Json;
using JosephHungerman.Data.Models;

namespace JosephHungerman.Data
{
    public class DbInitializer
    {
        public static async Task SeedResumeData(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
            if (!context.Resumes.Any())
            {
                using StreamReader reader = new(@"..\JosephHungerman.Data\Seeds\resumeseed.json");
                string json = await reader.ReadToEndAsync();
                Resume resume = JsonSerializer.Deserialize<Resume>(json);

                await context.AddAsync(resume);
                await context.SaveChangesAsync();
            }

            if (!context.Quotes.Any())
            {
                using StreamReader reader = new(@"..\JosephHungerman.Data\Seeds\quoteseed.json");
                string json = await reader.ReadToEndAsync();
                List<Quote> quotes = JsonSerializer.Deserialize<List<Quote>>(json);

                await context.AddRangeAsync(quotes);
                await context.SaveChangesAsync();
            }

            if (!context.Sections.Any())
            {
                using StreamReader reader = new(@"..\JosephHungerman.Data\Seeds\aboutseed.json");
                string json = await reader.ReadToEndAsync();
                List<Section> sections = JsonSerializer.Deserialize<List<Section>>(json);

                await context.AddRangeAsync(sections);
                await context.SaveChangesAsync();
            }
        }
    }
}
