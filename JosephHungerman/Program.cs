using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using JosephHungerman.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();

var endpoint = new Uri(Environment.GetEnvironmentVariable("KV_URI") ?? string.Empty);
builder.Configuration.AddAzureKeyVault(endpoint, new DefaultAzureCredential());

var test = builder.Configuration.GetConnectionString("jsh-stage");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
