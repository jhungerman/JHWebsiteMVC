using JosephHungerman.Extensions;

var builder = WebApplication.CreateBuilder(args);
//builder.Host.ConfigureAppConfiguration((_, config) =>
//{
//    var settings = config.Build();

//    var keyVaultEndpoint = settings["KV_URI"];

//    config.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential(), new KeyVaultSecretManager());
//});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices(builder.Configuration);

//var endpoint = new Uri(Environment.GetEnvironmentVariable("KV_URI") ?? string.Empty);
//builder.Configuration.AddAzureKeyVault(endpoint, new DefaultAzureCredential(), new KeyVaultSecretManager());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

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

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JosephHungerman v1");
});

app.Run();
